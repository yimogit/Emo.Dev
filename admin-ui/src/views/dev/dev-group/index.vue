﻿<template>
<div class="my-layout">
    <el-card class="my-search-box" shadow="never">
      <el-row>
        <el-col :span="18" :xs="24" class="my-search-box-inputs">
          <el-form :inline="true" label-width="auto" @submit.stop.prevent>
            <el-form-item class="my-search-box-item"  label="模板组名称">
              <el-input  clearable  v-model="state.filter.name" placeholder="" @keyup.enter="onQuery" >
              </el-input>
            </el-form-item>
            <el-form-item>
              <el-button type="primary" icon="ele-Search" @click="onQuery">查询</el-button>
            </el-form-item>
          </el-form>
        </el-col>
        <el-col :span="6" :xs="24" class="my-search-box-btns">
          <el-space>
          <el-button type="primary" v-auth="perms.add" icon="ele-Plus" @click="onAdd">新增</el-button>
            <el-dropdown :placement="'bottom-end'" v-if="auths([perms.batSoftDelete, perms.batDelete])">
              <el-button type="warning">批量操作 <el-icon><ele-ArrowDown /></el-icon></el-button>
              <template #dropdown>
                <el-dropdown-menu>
                    <el-dropdown-item v-if="auth(perms.batSoftDelete)" :disabled="state.sels.length==0" @click="onBatchSoftDelete" icon="ele-DeleteFilled">批量软删除</el-dropdown-item>
                    <el-dropdown-item v-if="auth(perms.batDelete)"  :disabled="state.sels.length==0" @click="onBatchDelete" icon="ele-Delete">批量删除</el-dropdown-item>
                </el-dropdown-menu>
              </template>
            </el-dropdown>
          </el-space>
        </el-col>
      </el-row>
    </el-card>

    <el-card class="my-fill mt8" shadow="never">
      <el-table v-loading="state.loading" :data="state.devGroupListData" row-key="id"  ref="listTableRef" @row-click="listTableToggleSelection"  @selection-change="selsChange">
        
          <el-table-column type="selection" width="50" />
          <el-table-column prop="name" label="模板组名称" show-overflow-tooltip width />
          <el-table-column prop="remark" label="备注" show-overflow-tooltip width />
          <el-table-column v-auths="[perms.update,perms.softDelete,perms.delete]" label="操作" :width="actionColWidth" fixed="right">
            <template #default="{ row }">
              <el-button v-auth="perms.update" icon="ele-EditPen"  text type="primary" @click.stop="onEdit(row)">编辑</el-button>
              <el-dropdown v-if="authAll([perms.delete,perms.softDelete])">
                <el-button icon="el-icon--right" text type="danger" >操作 <el-icon class="el-icon--right"><component :is="'ele-ArrowDown'" /></el-icon></el-button>
                <template #dropdown>
                  <el-dropdown-menu>
                    <el-dropdown-item v-if="auth(perms.softDelete)" @click.stop="onSoftDelete(row)" icon="ele-DeleteFilled">软删除</el-dropdown-item>
                    <el-dropdown-item v-if="auth(perms.delete)" @click.stop="onDelete(row)" icon="ele-Delete">删除</el-dropdown-item>
                  </el-dropdown-menu>
                </template>            
              </el-dropdown>
              <span v-else style="margin-left:5px;height:inherit">
                <el-button text type="warning" v-if="auth(perms.softDelete)" style="height:inherit" @click.stop="onDelete(row)" icon="ele-DeleteFilled">软删除</el-button>
                <el-button text type="danger" v-if="auth(perms.delete)" style="height:inherit" @click.stop="onDelete(row)" icon="ele-Delete">删除</el-button>
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
          background
          @size-change="onSizeChange"
          @current-change="onCurrentChange"
          layout="total, sizes, prev, pager, next, jumper"
        />
      </div>
    </el-card>

    <dev-group-form ref="devGroupFormRef" :title="state.devGroupFormTitle"></dev-group-form>
  </div>
</template>

<script lang="ts" setup name="dev/dev-group">
import { ref, reactive, onMounted, getCurrentInstance, onBeforeMount, defineAsyncComponent, computed } from 'vue'
import { PageInputDevGroupGetPageInput, DevGroupGetPageInput, DevGroupGetPageOutput, DevGroupGetOutput, DevGroupAddInput, DevGroupUpdateInput,
  DevGroupGetListInput, DevGroupGetListOutput,
} from '/@/api/dev/data-contracts'
import { DevGroupApi } from '/@/api/dev/DevGroup'
import eventBus from '/@/utils/mitt'
import { auth, auths, authAll } from '/@/utils/authFunction'

// 引入组件
const DevGroupForm = defineAsyncComponent(() => import('./components/dev-group-form.vue'))

const { proxy } = getCurrentInstance() as any

const devGroupFormRef = ref()
const listTableRef = ref()

//权限配置
const perms = {
  add:'api:dev:dev-group:add',
  update:'api:dev:dev-group:update',
  delete:'api:dev:dev-group:delete',
  batDelete:'api:dev:dev-group:batch-delete',
  softDelete:'api:dev:dev-group:soft-delete',
  batSoftDelete:'api:dev:dev-group:batch-soft-delete',
}

const actionColWidth = authAll([perms.update, perms.softDelete]) || authAll([perms.update, perms.delete]) ? 140 : 75

const state = reactive({
  loading: false,
  devGroupFormTitle: '',
  total: 0,
  sels: [] as Array<DevGroupGetPageOutput>,
  filter: {
    name: null,
  } as DevGroupGetPageInput | DevGroupGetListInput,
  pageInput: {
    currentPage: 1,
    pageSize: 20,
  } as PageInputDevGroupGetPageInput,
  devGroupListData: [] as Array<DevGroupGetListOutput>,
})

onMounted(() => {

  onQuery()
  eventBus.off('refreshDevGroup')
  eventBus.on('refreshDevGroup', async () => {
    onQuery()
  })
})

onBeforeMount(() => {
  eventBus.off('refreshDevGroup')
})



const onQuery = async () => {
  state.loading = true
  state.pageInput.filter = state.filter
  const res = await new DevGroupApi().getPage(state.pageInput).catch(() => {
    state.loading = false
  })

  state.devGroupListData = res?.data?.list ?? []
  state.total = res?.data?.total ?? 0
  state.loading = false



}

const onAdd = () => {
  state.devGroupFormTitle = '新增模板组'
  devGroupFormRef.value.open()
}

const onEdit = (row: DevGroupGetOutput) => {
  state.devGroupFormTitle = '编辑模板组'
  devGroupFormRef.value.open(row)
}

const onDelete = (row: DevGroupGetOutput) => {
  proxy.$modal
    .confirmDelete(`确定要删除【${row.name}】?`)
    .then(async () => {
      await new DevGroupApi().delete({ id: row.id }, { loading: true, showSuccessMessage: true })
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
const selsChange = (vals: DevGroupGetPageOutput[]) => {
  state.sels = vals
}

const onBatchDelete = async () => {
  proxy.$modal?.confirmDelete(`确定要删除选择的${state.sels.length}条记录？`).then(async () =>{
    const rst = await new DevGroupApi().batchDelete(state.sels?.map(item=>item.id) as number[], { loading: true, showSuccessMessage: true })
    if(rst?.success){
      onQuery()
    }
  })
}

const onSoftDelete = async (row: DevGroupGetOutput) => {
  proxy.$modal?.confirmDelete(`确定要移入回收站？`).then(async () =>{
    const rst = await new DevGroupApi().softDelete({ id: row.id }, { loading: true, showSuccessMessage: true })
    if(rst?.success){
      onQuery()
    }
  })
}

const onBatchSoftDelete = async () => {
  proxy.$modal?.confirmDelete(`确定要将选择的${state.sels.length}条记录移入回收站？`).then(async () =>{
    const rst = await new DevGroupApi().batchSoftDelete(state.sels?.map(item => item.id) as number[], { loading: true, showSuccessMessage: true })
    if(rst?.success){
      onQuery()
    }
  })
}
const listTableToggleSelection = async (row: any) => {
  listTableRef.value!.toggleRowSelection(row)
}
</script>
