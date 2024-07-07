<template>
  <my-layout class="my-layout">
    <pane size="30" min-size="20" max-size="35">
      <div class="my-flex-column w100 h100">
        <group-template-menu :groupIds="state.filter.groupIds" :projectId="state.filter.projectId"
          @node-click="onNodeClick" select-first-node></group-template-menu>
      </div>
    </pane>
    <pane size="70" v-loading="state.loading">
      <div class="my-flex-column w100 h100">
        <el-card class="mt8 my-fill" shadow="never" :body-style="{ paddingBottom: '0' }">
          <el-form label-position="top">
            <el-form-item label="生成路径">
              <el-input type="text" v-model="state.previewData.path"></el-input>
            </el-form-item>
            <el-form-item label="生成内容">
              <el-input type="textarea" v-model="state.previewData.content" :rows="60"></el-input>
            </el-form-item>
          </el-form>
        </el-card>
      </div>
    </pane>
  </my-layout>
</template>
  
<script lang="ts" setup name="dev/dev-project-gen/preview">
import { ref, reactive, onMounted, getCurrentInstance, onBeforeMount, defineAsyncComponent, computed } from 'vue'
import {
  DevProjectGenGenerateOutput,
  DevProjectGenPreviewMenuOutput
} from '/@/api/dev/data-contracts'
import { DevProjectGenApi } from '/@/api/dev/DevProjectGen'
import eventBus from '/@/utils/mitt'
import { auth, auths, authAll } from '/@/utils/authFunction'
import { useRoute, useRouter } from 'vue-router'
import { Pane } from 'splitpanes'

const GroupTemplateMenu = defineAsyncComponent(() => import('./components/dev-group-template-menu.vue'))
const MyLayout = defineAsyncComponent(() => import('/@/components/my-layout/index.vue'))

const route = useRoute()
const router = useRouter()
const { proxy } = getCurrentInstance() as any

const devTemplateFormRef = ref()
const listTableRef = ref()

//权限配置
const perms = {
  add: 'api:dev:dev-template:add',
  update: 'api:dev:dev-template:update',
  delete: 'api:dev:dev-template:delete',
  batDelete: 'api:dev:dev-template:batch-delete',
  softDelete: 'api:dev:dev-template:soft-delete',
  batSoftDelete: 'api:dev:dev-template:batch-soft-delete',
}

const state = reactive({
  loading: false,
  filter: {
    projectId: null,
    groupIds: null,
    templateIds: null,
  } as any,
  previewData: {
    path: null,
    content: null
  }
})

onMounted(() => {
  state.filter.projectId = route.query.projectId
  state.filter.groupIds = route.query.groupIds
})

onBeforeMount(() => {
})

const onQuery = async () => {
  state.loading = true
  const res = await new DevProjectGenApi().generate(state.filter, { showErrorMessage: false }).catch(() => {
    state.loading = false
  })
  if (res.data && res.data.length > 0) {
    state.previewData.path = res.data[0].path
    state.previewData.content = res.data[0].content
  } else {
    state.previewData.path = null
    state.previewData.content = res.msg
  }
  state.loading = false
}

const onNodeClick = (node: DevProjectGenPreviewMenuOutput | null) => {
  if (state.filter && node != null && !node.isGroup) {
    state.filter.templateIds = [node?.id]
    onQuery()
  }
}
</script>
  