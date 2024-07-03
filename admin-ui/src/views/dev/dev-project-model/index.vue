<template>
<div class="my-layout">
    <el-card class="mt8 search-box" shadow="never">
      <el-row>
        <el-col :span="18">
          <el-form :inline="true" @submit.stop.prevent>
            <el-form-item class="search-box-item"  label="所属项目">
              <el-select  clearable  v-model="state.filter.projectId" placeholder="" @keyup.enter="onQuery" >
                <el-option v-for="item in state.selectDevProjectListData" :key="item.id" :value="item.id" :label="item.name" />
              </el-select>
            </el-form-item>
            <el-form-item class="search-box-item"  label="模型名称">
              <el-input  clearable  v-model="state.filter.name" placeholder="" @keyup.enter="onQuery" >
              </el-input>
            </el-form-item>
            <el-form-item class="search-box-item"  label="模型编码">
              <el-input  clearable  v-model="state.filter.code" placeholder="" @keyup.enter="onQuery" >
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
      <el-table v-loading="state.loading" :data="state.devProjectModelListData" row-key="id" height="'100%'" style="width: 100%; height: 100%" @selection-change="selsChange">
        
          <el-table-column type="selection" width="50" />
          <el-table-column prop="projectId_Text" label="所属项目" show-overflow-tooltip width />
          <el-table-column prop="name" label="模型名称" show-overflow-tooltip width />
          <el-table-column prop="code" label="模型编码" show-overflow-tooltip width />
          <el-table-column prop="isDisable" label="是否禁用" show-overflow-tooltip width />
          <el-table-column prop="remark" label="备注" show-overflow-tooltip width />
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

    <dev-project-model-form ref="devProjectModelFormRef" :title="state.devProjectModelFormTitle"></dev-project-model-form>
  </div>
</template>

<script lang="ts" setup name="dev/dev-project-model">
import { ref, reactive, onMounted, getCurrentInstance, onBeforeMount, defineAsyncComponent, computed } from 'vue'
import { PageInputDevProjectModelGetPageInput, DevProjectModelGetPageInput, DevProjectModelGetPageOutput, DevProjectModelGetOutput, DevProjectModelAddInput, DevProjectModelUpdateInput,
  DevProjectModelGetListInput, DevProjectModelGetListOutput,
  DevProjectGetListOutput,
  DevProjectGetOutput,                    
} from '/@/api/dev/data-contracts'
import { DevProjectModelApi } from '/@/api/dev/DevProjectModel'
import { DevProjectApi } from '/@/api/dev/DevProject'
import eventBus from '/@/utils/mitt'
import { auth, auths, authAll } from '/@/utils/authFunction'

// 引入组件
const DevProjectModelForm = defineAsyncComponent(() => import('./components/dev-project-model-form.vue'))

const { proxy } = getCurrentInstance() as any

const devProjectModelFormRef = ref()

//权限配置
const perms = {
  add:'api:dev:dev-project-model:add',
  update:'api:dev:dev-project-model:update',
  delete:'api:dev:dev-project-model:delete',
  batDelete:'api:dev:dev-project-model:batch-delete',
  softDelete:'api:dev:dev-project-model:soft-delete',
  batSoftDelete:'api:dev:dev-project-model:batch-soft-delete',
}

const actionColWidth = authAll([perms.update, perms.softDelete]) || authAll([perms.update, perms.delete]) ? 135 : 70

const state = reactive({
  loading: false,
  devProjectModelFormTitle: '',
  total: 0,
  sels: [] as Array<DevProjectModelGetPageOutput>,
  filter: {
    projectId: null,
    name: null,
    code: null,
  } as DevProjectModelGetPageInput | DevProjectModelGetListInput,
  pageInput: {
    currentPage: 1,
    pageSize: 20,
  } as PageInputDevProjectModelGetPageInput,
  devProjectModelListData: [] as Array<DevProjectModelGetListOutput>,
  selectDevProjectListData: [] as DevProjectGetListOutput[],
})

onMounted(() => {

  getDevProjectList();
  onQuery()
  eventBus.off('refreshDevProjectModel')
  eventBus.on('refreshDevProjectModel', async () => {
    onQuery()
  })
})

onBeforeMount(() => {
  eventBus.off('refreshDevProjectModel')
})

const getDevProjectList = async () => {
  const res = await new DevProjectApi().getList({}).catch(() => {
    state.selectDevProjectListData = []
  })
  state.selectDevProjectListData = res?.data || []
}


const onQuery = async () => {
  state.loading = true
  state.pageInput.filter = state.filter
  const res = await new DevProjectModelApi().getPage(state.pageInput).catch(() => {
    state.loading = false
  })

  state.devProjectModelListData = res?.data?.list ?? []
  state.total = res?.data?.total ?? 0
  state.loading = false



}

const onAdd = () => {
  state.devProjectModelFormTitle = '新增项目模型'
  devProjectModelFormRef.value.open()
}

const onEdit = (row: DevProjectModelGetOutput) => {
  state.devProjectModelFormTitle = '编辑项目模型'
  devProjectModelFormRef.value.open(row)
}

const onDelete = (row: DevProjectModelGetOutput) => {
  proxy.$modal
    .confirmDelete(`确定要删除【${row.name}】?`)
    .then(async () => {
      await new DevProjectModelApi().delete({ id: row.id }, { loading: true, showSuccessMessage: true })
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
const selsChange = (vals: DevProjectModelGetPageOutput[]) => {
  state.sels = vals
}

const onBatchDelete = async () => {
  proxy.$modal?.confirmDelete(`确定要删除选择的${state.sels.length}条记录？`).then(async () =>{
    const rst = await new DevProjectModelApi().batchDelete(state.sels?.map(item=>item.id) as number[], { loading: true, showSuccessMessage: true })
    if(rst?.success){
      onQuery()
    }
  })
}

const onSoftDelete = async (row: DevProjectModelGetOutput) => {
  proxy.$modal?.confirmDelete(`确定要移入回收站？`).then(async () =>{
    const rst = await new DevProjectModelApi().softDelete({ id: row.id }, { loading: true, showSuccessMessage: true })
    if(rst?.success){
      onQuery()
    }
  })
}

const onBatchSoftDelete = async () => {
  proxy.$modal?.confirmDelete(`确定要将选择的${state.sels.length}条记录移入回收站？`).then(async () =>{
    const rst = await new DevProjectModelApi().batchSoftDelete(state.sels?.map(item => item.id) as number[], { loading: true, showSuccessMessage: true })
    if(rst?.success){
      onQuery()
    }
  })
}
</script>
