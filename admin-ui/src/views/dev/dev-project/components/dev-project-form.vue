﻿<template>
  <div>
    <el-dialog v-model="state.showDialog" :title="title" draggable destroy-on-close :close-on-click-modal="false"
      :close-on-press-escape="false" class="my-dialog-model" :overflow="true">
      <el-form ref="formRef" :model="form" label-width="auto" @submit="onSure" v-zoom="'.my-dialog-model'">
        <el-row :gutter="20">
        <el-col :span="12" :xs="24">
           <el-form-item label="项目名称" prop="name" v-show="editItemIsShow(true, true)">
             <el-input  v-model="state.form.name" placeholder="" >
             </el-input>
           </el-form-item>
        </el-col>
        <el-col :span="12" :xs="24">
           <el-form-item label="项目编码" prop="code" v-show="editItemIsShow(true, true)">
             <el-input  v-model="state.form.code" placeholder="" >
             </el-input>
           </el-form-item>
        </el-col>
        <el-col :span="12" :xs="24">
           <el-form-item label="是否启用" prop="isEnable" v-show="editItemIsShow(true, true)">
             <el-checkbox  v-model="state.form.isEnable" placeholder="" >
             </el-checkbox>
           </el-form-item>
        </el-col>
        <el-col :span="12" :xs="24">
           <el-form-item label="使用模板组" prop="groupId" v-show="editItemIsShow(true, true)">
             <el-select  v-model="state.form.groupId" placeholder="" >
               <el-option v-for="item in state.selectDevGroupListData" :key="item.id" :value="item.id" :label="item.name" />
             </el-select>
           </el-form-item>
        </el-col>
        <el-col :span="24" :xs="24">
           <el-form-item label="备注" prop="remark" v-show="editItemIsShow(true, true)">
             <el-input  type="textarea"  v-model="state.form.remark" placeholder="" >
             </el-input>
           </el-form-item>
        </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="onCancel" >取 消</el-button>
          <el-button type="primary" @click="onSure" :loading="state.sureLoading">确 定</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script lang="ts" setup name="dev/dev-project/form">
import { reactive, toRefs, getCurrentInstance, ref, defineAsyncComponent} from 'vue'
import { DevProjectAddInput, DevProjectUpdateInput,
  DevProjectGetListInput, DevProjectGetListOutput,
  DevGroupGetListOutput,
  DevGroupGetOutput,
} from '/@/api/dev/data-contracts'
import { DevProjectApi } from '/@/api/dev/DevProject'
import { DevGroupApi } from '/@/api/dev/DevGroup'
import { auth, auths, authAll } from '/@/utils/authFunction'


import eventBus from '/@/utils/mitt'

defineProps({
  title: {
    type: String,
    default: '',
  },
})

const { proxy } = getCurrentInstance() as any

const formRef = ref()
const state = reactive({
  showDialog: false,
  sureLoading: false,
  form: {} as DevProjectAddInput | DevProjectUpdateInput | any,
  selectDevGroupListData: [] as DevGroupGetListOutput[],
})
const { form } = toRefs(state)

// 打开对话框
const open = async (row: any = {}) => {
    
  getDevGroupList();


  if (row.id > 0) {
    const res = await new DevProjectApi().get({ id: row.id }, { loading: true }).catch(() => {
      proxy.$modal.closeLoading()
    })

    if (res?.success) {
      state.form = res.data as DevProjectUpdateInput
    }
  } else {
    state.form = defaultToAdd()
  }
  state.showDialog = true
}

const getDevGroupList = async () => {
  const res = await new DevGroupApi().getList({}).catch(() => {
    state.selectDevGroupListData = []
  })
  state.selectDevGroupListData = res?.data || []
}


const defaultToAdd = (): DevProjectAddInput => {
  return {
    name: "",
    code: "",
    isEnable: false,
    groupId: 0,
    remark: null,
  } as DevProjectAddInput
}

// 取消
const onCancel = () => {
  state.showDialog = false
}

// 确定
const onSure = () => {
  formRef.value.validate(async (valid: boolean) => {
    if (!valid) return

    state.sureLoading = true
    let res = {} as any
    if (state.form.id != undefined && state.form.id > 0) {
      res = await new DevProjectApi().update(state.form, { showSuccessMessage: true }).catch(() => {
        state.sureLoading = false
      })
    } else {
      res = await new DevProjectApi().add(state.form, { showSuccessMessage: true }).catch(() => {
        state.sureLoading = false
      })
    }
    state.sureLoading = false

    if (res?.success) {
      eventBus.emit('refreshDevProject')
      state.showDialog = false
    }
  })
}

const editItemIsShow = (add: Boolean, edit: Boolean): Boolean => {
    if(add && edit)return true;
    let isEdit=state.form.id != undefined && state.form.id > 0
    if(add && !isEdit)return true;
    if(edit && isEdit)return true;
    return false;
}


defineExpose({
  open,
})
</script>
