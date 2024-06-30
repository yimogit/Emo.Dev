<template>
  <div>
    <el-dialog v-model="state.showDialog" :title="title" draggable destroy-on-close :close-on-click-modal="false"
      :close-on-press-escape="false" class="my-dialog-form">
      <el-form ref="formRef" :model="form" size="default" label-width="auto">
        <el-row :gutter="20">
        <el-col :span="12">
           <el-form-item label="字段名称" prop="name" v-show="editItemIsShow(true, true)">
             <el-input  v-model="state.form.name" placeholder="" >
             </el-input>
           </el-form-item>
        </el-col>
        <el-col :span="12">
           <el-form-item label="字段描述" prop="description" v-show="editItemIsShow(true, true)">
             <el-input  v-model="state.form.description" placeholder="" >
             </el-input>
           </el-form-item>
        </el-col>
        <el-col :span="12">
           <el-form-item label="字段类型" prop="dataType" v-show="editItemIsShow(true, true)">
             <el-input  v-model="state.form.dataType" placeholder="" >
             </el-input>
           </el-form-item>
        </el-col>
        <el-col :span="12">
           <el-form-item label="是否必填" prop="isRequired" v-show="editItemIsShow(true, true)">
             <el-input  v-model="state.form.isRequired" placeholder="" >
             </el-input>
           </el-form-item>
        </el-col>
        <el-col :span="12">
           <el-form-item label="最大长度" prop="maxLength" v-show="editItemIsShow(true, true)">
             <el-input-number  v-model="state.form.maxLength" placeholder="" >
             </el-input-number>
           </el-form-item>
        </el-col>
        <el-col :span="12">
           <el-form-item label="最小长度" prop="minLength" v-show="editItemIsShow(true, true)">
             <el-input-number  v-model="state.form.minLength" placeholder="" >
             </el-input-number>
           </el-form-item>
        </el-col>
        <el-col :span="12">
           <el-form-item label="模型Id" prop="modelId" v-show="editItemIsShow(true, true)">
             <el-select  v-model="state.form.modelId" placeholder="" >
               <el-option v-for="item in state.selectDevProjectModelListData" :key="item.id" :value="item.id" :label="item.name" />
             </el-select>
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

<script lang="ts" setup name="dev/dev-project-model-field/form">
import { reactive, toRefs, getCurrentInstance, ref, defineAsyncComponent} from 'vue'
import { DevProjectModelFieldAddInput, DevProjectModelFieldUpdateInput,
  DevProjectModelFieldGetListInput, DevProjectModelFieldGetListOutput,
  DevProjectModelGetListOutput,
  DevProjectModelGetOutput,
} from '/@/api/dev/data-contracts'
import { DevProjectModelFieldApi } from '/@/api/dev/DevProjectModelField'
import { DevProjectModelApi } from '/@/api/dev/DevProjectModel'
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
  form: {} as DevProjectModelFieldAddInput | DevProjectModelFieldUpdateInput | any,
  selectDevProjectModelListData: [] as DevProjectModelGetListOutput[],
})
const { form } = toRefs(state)

// 打开对话框
const open = async (row: any = {}) => {
    
  getDevProjectModelList();


  if (row.id > 0) {
    const res = await new DevProjectModelFieldApi().get({ id: row.id }, { loading: true }).catch(() => {
      proxy.$modal.closeLoading()
    })

    if (res?.success) {
      state.form = res.data as DevProjectModelFieldUpdateInput
    }
  } else {
    state.form = defaultToAdd()
  }
  state.showDialog = true
}

const getDevProjectModelList = async () => {
  const res = await new DevProjectModelApi().getList({}).catch(() => {
    state.selectDevProjectModelListData = []
  })
  state.selectDevProjectModelListData = res?.data || []
}


const defaultToAdd = (): DevProjectModelFieldAddInput => {
  return {
    name: "",
    description: null,
    dataType: null,
    isRequired: null,
    maxLength: null,
    minLength: null,
    modelId: 0,
  } as DevProjectModelFieldAddInput
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
      res = await new DevProjectModelFieldApi().update(state.form, { showSuccessMessage: true }).catch(() => {
        state.sureLoading = false
      })
    } else {
      res = await new DevProjectModelFieldApi().add(state.form, { showSuccessMessage: true }).catch(() => {
        state.sureLoading = false
      })
    }
    state.sureLoading = false

    if (res?.success) {
      eventBus.emit('refreshDevProjectModelField')
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
