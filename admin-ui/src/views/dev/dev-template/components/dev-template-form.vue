<template>
  <div>
    <el-dialog v-model="state.showDialog" :title="title" draggable destroy-on-close :close-on-click-modal="false"
      :close-on-press-escape="false" class="my-dialog-form">
      <el-form ref="formRef" :model="form" size="default" label-width="auto" @submit="onSure">
        <el-row :gutter="20">
        <el-col :span="12">
           <el-form-item label="模板名称" prop="name" v-show="editItemIsShow(true, true)">
             <el-input  v-model="state.form.name" placeholder="" >
             </el-input>
           </el-form-item>
        </el-col>
        <el-col :span="12">
           <el-form-item label="模板分组" prop="groupId" v-show="editItemIsShow(true, true)">
             <el-select  v-model="state.form.groupId" placeholder="" >
               <el-option v-for="item in state.selectDevGroupListData" :key="item.id" :value="item.id" :label="item.name" />
             </el-select>
           </el-form-item>
        </el-col>
        <el-col :span="12">
           <el-form-item label="生成路径" prop="outTo" v-show="editItemIsShow(true, true)">
             <el-input  v-model="state.form.outTo" placeholder="" >
             </el-input>
           </el-form-item>
        </el-col>
        <el-col :span="12">
           <el-form-item label="是否禁用" prop="isDisable" v-show="editItemIsShow(true, true)">
             <el-checkbox  v-model="state.form.isDisable" placeholder="" >
             </el-checkbox>
           </el-form-item>
        </el-col>
        <el-col :span="24">
           <el-form-item label="模板内容" prop="content" v-show="editItemIsShow(true, true)">
             <el-input  type="textarea"  v-model="state.form.content" placeholder="" >
             </el-input>
           </el-form-item>
        </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="onCancel" size="default">取 消</el-button>
          <el-button type="primary" @click="onSure" size="default" :loading="state.sureLoading">确 定</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script lang="ts" setup name="dev/dev-template/form">
import { reactive, toRefs, getCurrentInstance, ref, defineAsyncComponent} from 'vue'
import { DevTemplateAddInput, DevTemplateUpdateInput,
  DevTemplateGetListInput, DevTemplateGetListOutput,
  DevGroupGetListOutput,
  DevGroupGetOutput,
} from '/@/api/dev/data-contracts'
import { DevTemplateApi } from '/@/api/dev/DevTemplate'
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
  form: {} as DevTemplateAddInput | DevTemplateUpdateInput | any,
  selectDevGroupListData: [] as DevGroupGetListOutput[],
})
const { form } = toRefs(state)

// 打开对话框
const open = async (row: any = {}) => {
    
  getDevGroupList();


  if (row.id > 0) {
    const res = await new DevTemplateApi().get({ id: row.id }, { loading: true }).catch(() => {
      proxy.$modal.closeLoading()
    })

    if (res?.success) {
      state.form = res.data as DevTemplateUpdateInput
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


const defaultToAdd = (): DevTemplateAddInput => {
  return {
    name: "",
    groupId: 0,
    outTo: null,
    isDisable: false,
    content: "",
  } as DevTemplateAddInput
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
      res = await new DevTemplateApi().update(state.form, { showSuccessMessage: true }).catch(() => {
        state.sureLoading = false
      })
    } else {
      res = await new DevTemplateApi().add(state.form, { showSuccessMessage: true }).catch(() => {
        state.sureLoading = false
      })
    }
    state.sureLoading = false

    if (res?.success) {
      eventBus.emit('refreshDevTemplate')
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
