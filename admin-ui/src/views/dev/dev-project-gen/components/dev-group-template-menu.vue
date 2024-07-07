<template>
  <el-card shadow="never" style="margin-top: 8px" body-style="padding:0px;" class="my-fill">
    <template #header>
      <el-input v-model="state.filterText" placeholder="筛选模板" clearable />

    </template>
    <el-scrollbar v-loading="state.loading" height="100%" max-height="100%" :always="false"
      wrap-style="padding:var(--el-card-padding)">
      <el-tree ref="menuRef" :data="state.treeData" node-key="id" :props="{ children: 'children', label: 'name' }"
        :filter-node-method="onFilterNode" highlight-current check-strictly default-expand-all render-after-expand
        :expand-on-click-node="false" v-bind="$attrs" @node-click="onNodeClick" @check-change="onCheckChange">
        <template #default="{ node, data }">
          <span class="custom-tree-node">
            <span v-if="!data.isGroup">
              <el-icon class="el-icon--right" @click.stop="editTemplate(node, data)">
                <ele-Edit />
              </el-icon>
            </span>
            <span title="点击图标可编辑模板">{{ node.label }}</span>
          </span>
        </template>
      </el-tree>
    </el-scrollbar>
    <dev-template-form ref="devTemplateFormRef" :title="'编辑模板'"></dev-template-form>
  </el-card>
</template>

<script lang="ts" setup name="dev/project/gen/grouptemplatemenu">
import { onMounted, reactive, ref, watch, nextTick, defineAsyncComponent } from 'vue'
import { DevProjectGenApi } from '/@/api/dev/DevProjectGen'
import {
  DevProjectGenPreviewMenuOutput,
} from '/@/api/dev/data-contracts'
import eventBus from '/@/utils/mitt'
import { listToTree } from '/@/utils/tree'
import { ElTree } from 'element-plus'
// 引入组件
const DevTemplateForm = defineAsyncComponent(() => import('../../dev-template/components/dev-template-form.vue'))

interface Props {
  modelValue: number[] | null | undefined
  selectFirstNode: boolean,
  projectId: number,
  groupIds: number[]
}

const props = withDefaults(defineProps<Props>(), {
  modelValue: () => [],
  selectFirstNode: false,
  projectId: 0,
  groupIds: () => []
})

const devTemplateFormRef = ref()
const menuRef = ref<InstanceType<typeof ElTree>>()
const state = reactive({
  loading: false,
  filterText: '',
  treeData: [] as Array<DevProjectGenPreviewMenuOutput>,
  lastKey: 0,
})

watch(
  () => state.filterText,
  (val) => {
    menuRef.value?.filter(val)
  }
)

onMounted(() => {
  initData()
  eventBus.off('refreshDevTemplate')
  eventBus.on('refreshDevTemplate', async () => {
    initData()
    if (state.lastKey) {
      menuRef.value?.setCurrentKey(state.lastKey)
      let node = menuRef.value?.getNode(state.lastKey)
      emits('node-click', node?.data)
    }
  })
})

const emits = defineEmits<{
  (e: 'node-click', node: DevProjectGenPreviewMenuOutput | null): void
  (e: 'update:modelValue', node: any[] | undefined | null): void
}>()

const onFilterNode = (value: string, data: DevProjectGenPreviewMenuOutput) => {
  if (!value) return true
  return data.name?.indexOf(value) !== -1
}

const onNodeClick = (node: DevProjectGenPreviewMenuOutput) => {
  if (state.lastKey === node.id) {
    state.lastKey = 0
    menuRef.value?.setCurrentKey(undefined)
    emits('node-click', null)
  } else {
    state.lastKey = node.id as number
    emits('node-click', node)
  }
}

const onCheckChange = () => {
  emits('update:modelValue', menuRef.value?.getCheckedKeys())
}

const initData = async () => {
  state.loading = true
  const res = await new DevProjectGenApi().getPreviewMenu({ projectId: props.projectId, groupIds: props.groupIds }).catch(() => {
    state.loading = false
  })
  state.loading = false
  if (res?.success && res.data && res.data.length > 0) {
    state.treeData = res.data.map(s => {
      return {
        id: s.groupId,
        name: s.groupName,
        isGroup: true,
        children: s.templateList.map(s2 => {
          return {
            id: s2.templateId,
            name: s2.templateName,
            parentId: s2.groupId,
          }
        })
      }
    })
    if (state.treeData.length > 0 && props.selectFirstNode) {
      nextTick(() => {
        // const firstNode = state.treeData[0]
        // menuRef.value?.setCurrentKey(firstNode.id)
        // emits('node-click', firstNode)
      })
    }
  } else {
    state.treeData = []
  }
}
const editTemplate = (node, data) => {
  devTemplateFormRef.value.open({
    id: data.id
  })
}

defineExpose({
  menuRef,
})
</script>
