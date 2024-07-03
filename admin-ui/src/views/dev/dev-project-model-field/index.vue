<template>
<div class="my-layout">
    <el-card class="mt8 search-box" shadow="never">
      <el-row>
        <el-col :span="18">
          <el-form :inline="true" @submit.stop.prevent>
            <el-form-item class="search-box-item"  label="所属模型">
              <el-select    clearable  v-model="state.filter.modelId" placeholder="" @keyup.enter="onQuery" >
                <el-option v-for="item in state.selectDevProjectModelListData" :key="item.id" :value="item.id" :label="item.name" />
              </el-select>
            </el-form-item>
            <el-form-item class="search-box-item"  label="字段名称">
              <el-input  clearable  v-model="state.filter.name" placeholder="" @keyup.enter="onQuery" >
              </el-input>
            </el-form-item>
            <el-form-item>
              <el-button type="primary" icon="ele-Search" @click="onQuery">查询</el-button>
            </el-form-item>
          </el-form>
        </el-col>
        <el-col :span="6" class="text-right">
          <el-space>
          <el-button type="primary" v-auth="perms.add" icon="ele-Plus" @click="onAdd">新增</el-button>
            <el-dropdown :placement="'bottom-end'" v-if="auths([perms.batSoftDelete, perms.batDelete])">
              <el-button type="warning">批量操作 <el-icon><ele-ArrowDown /></el-icon></el-button>
              <template #dropdown>
                <el-dropdown-menu>
                    <el-dropdown-item v-if="auth(perms.batSoftDelete)" :disabled="state.sels.length==0" @click="onBatchSoftDelete" icon="ele-DeleteFilled">批量删除</el-dropdown-item>
                    <el-dropdown-item v-if="auth(perms.batDelete)"  :disabled="state.sels.length==0" @click="onBatchDelete" icon="ele-Delete">批量删除</el-dropdown-item>
                </el-dropdown-menu>
              </template>
            </el-dropdown>
          </el-space>
        </el-col>
      </el-row>
    </el-card>

    <el-card class="my-fill mt8" shadow="never">
      <el-table v-loading="state.loading" :data="state.devProjectModelFieldListData" row-key="id" height="'100%'" style="width: 100%; height: 100%" @selection-change="selsChange">
        
          <el-table-column type="selection" width="50" />
          <el-table-column prop="modelId_Text" label="所属模型" show-overflow-tooltip width />
          <el-table-column prop="name" label="字段名称" show-overflow-tooltip width />
          <el-table-column prop="code" label="字段编码" show-overflow-tooltip width />
          <el-table-column prop="dataTypeDictName" label="字段类型" show-overflow-tooltip width />
          <el-table-column prop="isRequired" label="是否必填" show-overflow-tooltip width />
          <el-table-column prop="maxLength" label="最大长度" show-overflow-tooltip width />
          <el-table-column prop="minLength" label="最小长度" show-overflow-tooltip width />
          <el-table-column prop="sort" label="字段顺序" show-overflow-tooltip width />
          <el-table-column prop="description" label="字段描述" show-overflow-tooltip width />
          <el-table-column prop="propertiesDictName" label="字段属性" show-overflow-tooltip width />
          <el-table-column v-auths="[perms.update,perms.softDelete,perms.delete]" label="操作" :width="actionColWidth" fixed="right">
            <template #default="{ row }">
              <el-button v-auth="perms.update" icon="ele-EditPen" size="small" text type="primary" @click="onEdit(row)">编辑</el-button>
              <el-dropdown v-if="authAll([perms.delete,perms.softDelete])">
                <el-button icon="el-icon--right" size="small" text type="danger" >操作 <el-icon class="el-icon--right"><component :is="'ele-ArrowDown'" /></el-icon></el-button>
                <template #dropdown>
                  <el-dropdown-menu>
                    <el-dropdown-item v-if="auth(perms.delete)" @click="onDelete(row)" icon="ele-Delete">删除</el-dropdown-item>
                    <el-dropdown-item v-if="auth(perms.softDelete)" @click="onSoftDelete(row)" icon="ele-DeleteFilled">软删除</el-dropdown-item>
                  </el-dropdown-menu>
                </template>            
              </el-dropdown>
              <span v-else style="margin-left:5px;height:inherit">
                <el-button text type="warning" v-if="auth(perms.softDelete)" style="height:inherit" @click="onDelete(row)" icon="ele-DeleteFilled">软删除</el-button>
                <el-button text type="danger" v-if="auth(perms.delete)" style="height:inherit" @click="onDelete(row)" icon="ele-Delete">删除</el-button>
              </span>
            </template>
          </el-table-column>
      </el-table>

      <div class="my-flex my-flex-end" style="margin-top: 20px">
        <el-pagination
          v-model:currentPage="state.pageInput.currentPage"
          v-model:page-size="state.pageInput.pageSize"
          :total="state.total"
          :page-sizes="[10, 20, 50, 100]"
          small
          background
          @size-change="onSizeChange"
          @current-change="onCurrentChange"
          layout="total, sizes, prev, pager, next, jumper"
        />
      </div>
    </el-card>

    <dev-project-model-field-form ref="devProjectModelFieldFormRef" :title="state.devProjectModelFieldFormTitle"></dev-project-model-field-form>
  </div>
</template>

<script lang="ts" setup name="dev/dev-project-model-field">
import { ref, reactive, onMounted, getCurrentInstance, onBeforeMount, defineAsyncComponent, computed } from 'vue'
import { PageInputDevProjectModelFieldGetPageInput, DevProjectModelFieldGetPageInput, DevProjectModelFieldGetPageOutput, DevProjectModelFieldGetOutput, DevProjectModelFieldAddInput, DevProjectModelFieldUpdateInput,
  DevProjectModelFieldGetListInput, DevProjectModelFieldGetListOutput,
  DevProjectModelGetListOutput,
  DevProjectModelGetOutput,                    
} from '/@/api/dev/data-contracts'
import { DevProjectModelFieldApi } from '/@/api/dev/DevProjectModelField'
import { DevProjectModelApi } from '/@/api/dev/DevProjectModel'
import { DictApi } from '/@/api/admin/Dict'
import eventBus from '/@/utils/mitt'
import { auth, auths, authAll } from '/@/utils/authFunction'

// 引入组件
const DevProjectModelFieldForm = defineAsyncComponent(() => import('./components/dev-project-model-field-form.vue'))

const { proxy } = getCurrentInstance() as any

const devProjectModelFieldFormRef = ref()

//权限配置
const perms = {
  add:'api:dev:dev-project-model-field:add',
  update:'api:dev:dev-project-model-field:update',
  delete:'api:dev:dev-project-model-field:delete',
  batDelete:'api:dev:dev-project-model-field:batch-delete',
  softDelete:'api:dev:dev-project-model-field:soft-delete',
  batSoftDelete:'api:dev:dev-project-model-field:batch-soft-delete',
}

const actionColWidth = authAll([perms.update, perms.softDelete]) || authAll([perms.update, perms.delete]) ? 135 : 70

const state = reactive({
  loading: false,
  devProjectModelFieldFormTitle: '',
  total: 0,
  sels: [] as Array<DevProjectModelFieldGetPageOutput>,
  filter: {
    modelId: null,
    name: null,
  } as DevProjectModelFieldGetPageInput | DevProjectModelFieldGetListInput,
  pageInput: {
    currentPage: 1,
    pageSize: 20,
  } as PageInputDevProjectModelFieldGetPageInput,
  devProjectModelFieldListData: [] as Array<DevProjectModelFieldGetListOutput>,
  selectDevProjectModelListData: [] as DevProjectModelGetListOutput[],
  //字典相关
  dicts:{
    "fieldType":[],   
    "fieldProperties":[],   
  }
})

onMounted(() => {

  getDevProjectModelList();
  getDictsTree()      
  onQuery()
  eventBus.off('refreshDevProjectModelField')
  eventBus.on('refreshDevProjectModelField', async () => {
    onQuery()
  })
})

onBeforeMount(() => {
  eventBus.off('refreshDevProjectModelField')
})

const getDevProjectModelList = async () => {
  const res = await new DevProjectModelApi().getList({}).catch(() => {
    state.selectDevProjectModelListData = []
  })
  state.selectDevProjectModelListData = res?.data || []
}

//获取需要使用的字典树
const getDictsTree = async () => {
  let res = await new DictApi().getList(['fieldType','fieldProperties'])
  if(!res?.success)return;
    state.dicts = res.data
}

const onQuery = async () => {
  state.loading = true
  state.pageInput.filter = state.filter
  const res = await new DevProjectModelFieldApi().getPage(state.pageInput).catch(() => {
    state.loading = false
  })

  state.devProjectModelFieldListData = res?.data?.list ?? []
  state.total = res?.data?.total ?? 0
  state.loading = false



}

const onAdd = () => {
  state.devProjectModelFieldFormTitle = '新增项目模型字段'
  devProjectModelFieldFormRef.value.open()
}

const onEdit = (row: DevProjectModelFieldGetOutput) => {
  state.devProjectModelFieldFormTitle = '编辑项目模型字段'
  devProjectModelFieldFormRef.value.open(row)
}

const onDelete = (row: DevProjectModelFieldGetOutput) => {
  proxy.$modal
    .confirmDelete(`确定要删除【${row.name}】?`)
    .then(async () => {
      await new DevProjectModelFieldApi().delete({ id: row.id }, { loading: true, showSuccessMessage: true })
      onQuery()
    })
    .catch(() => {})
}

const onSizeChange = (val: number) => {
  state.pageInput.pageSize = val
  onQuery()
}

const onCurrentChange = (val: number) => {
  state.pageInput.currentPage = val
  onQuery()
}
const selsChange = (vals: DevProjectModelFieldGetPageOutput[]) => {
  state.sels = vals
}

const onBatchDelete = async () => {
  proxy.$modal?.confirmDelete(`确定要删除选择的${state.sels.length}条记录？`).then(async () =>{
    const rst = await new DevProjectModelFieldApi().batchDelete(state.sels?.map(item=>item.id) as number[], { loading: true, showSuccessMessage: true })
    if(rst?.success){
      onQuery()
    }
  })
}

const onSoftDelete = async (row: DevProjectModelFieldGetOutput) => {
  proxy.$modal?.confirmDelete(`确定要移入回收站？`).then(async () =>{
    const rst = await new DevProjectModelFieldApi().softDelete({ id: row.id }, { loading: true, showSuccessMessage: true })
    if(rst?.success){
      onQuery()
    }
  })
}

const onBatchSoftDelete = async () => {
  proxy.$modal?.confirmDelete(`确定要将选择的${state.sels.length}条记录移入回收站？`).then(async () =>{
    const rst = await new DevProjectModelFieldApi().batchSoftDelete(state.sels?.map(item => item.id) as number[], { loading: true, showSuccessMessage: true })
    if(rst?.success){
      onQuery()
    }
  })
}
</script>
