/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

/** 模板组新增输入 */
export interface DevGroupAddInput {
  /**
   * 模板组名称
   * @minLength 1
   */
  name: string
  /** 备注 */
  remark?: string | null
}

/** 模板组列表查询条件输入 */
export interface DevGroupGetListInput {
  /** 模板组名称 */
  name?: string | null
}

/** 模板组列表查询结果输出 */
export interface DevGroupGetListOutput {
  /** @format int64 */
  id?: number
  /** @format date-time */
  createdTime?: string
  createdUserName?: string | null
  modifiedUserName?: string | null
  /** @format date-time */
  modifiedTime?: string | null
  /** 模板组名称 */
  name?: string | null
  /** 备注 */
  remark?: string | null
}

/** 模板组查询结果输出 */
export interface DevGroupGetOutput {
  /** @format int64 */
  id?: number
  /** 模板组名称 */
  name?: string | null
  /** 备注 */
  remark?: string | null
}

/** 模板组分页查询条件输入 */
export interface DevGroupGetPageInput {
  /** 模板组名称 */
  name?: string | null
}

/** 模板组分页查询结果输出 */
export interface DevGroupGetPageOutput {
  /** @format int64 */
  id?: number
  /** @format date-time */
  createdTime?: string
  createdUserName?: string | null
  modifiedUserName?: string | null
  /** @format date-time */
  modifiedTime?: string | null
  /** 模板组名称 */
  name?: string | null
  /** 备注 */
  remark?: string | null
}

/** 模板组更新数据输入 */
export interface DevGroupUpdateInput {
  /** @format int64 */
  id?: number
  /**
   * 模板组名称
   * @minLength 1
   */
  name: string
  /** 备注 */
  remark?: string | null
}

/** 项目新增输入 */
export interface DevProjectAddInput {
  /**
   * 项目名称
   * @minLength 1
   */
  name: string
  /**
   * 项目编码
   * @minLength 1
   */
  code: string
  /** 备注 */
  remark?: string | null
}

/** 项目列表查询条件输入 */
export interface DevProjectGetListInput {
  /** 项目名称 */
  name?: string | null
  /** 项目编码 */
  code?: string | null
}

/** 项目列表查询结果输出 */
export interface DevProjectGetListOutput {
  /** @format int64 */
  id?: number
  /** @format date-time */
  createdTime?: string
  createdUserName?: string | null
  modifiedUserName?: string | null
  /** @format date-time */
  modifiedTime?: string | null
  /** 项目名称 */
  name?: string | null
  /** 项目编码 */
  code?: string | null
  /** 备注 */
  remark?: string | null
}

/** 项目查询结果输出 */
export interface DevProjectGetOutput {
  /** @format int64 */
  id?: number
  /** 项目名称 */
  name?: string | null
  /** 项目编码 */
  code?: string | null
  /** 备注 */
  remark?: string | null
}

/** 项目分页查询条件输入 */
export interface DevProjectGetPageInput {
  /** 项目名称 */
  name?: string | null
  /** 项目编码 */
  code?: string | null
}

/** 项目分页查询结果输出 */
export interface DevProjectGetPageOutput {
  /** @format int64 */
  id?: number
  /** @format date-time */
  createdTime?: string
  createdUserName?: string | null
  modifiedUserName?: string | null
  /** @format date-time */
  modifiedTime?: string | null
  /** 项目名称 */
  name?: string | null
  /** 项目编码 */
  code?: string | null
  /** 备注 */
  remark?: string | null
}

/** 项目模型新增输入 */
export interface DevProjectModelAddInput {
  /**
   * 模型名称
   * @minLength 1
   */
  name: string
  /**
   * 模型编码
   * @minLength 1
   */
  code: string
  /** 备注 */
  remark?: string | null
  /**
   * 所属项目
   * @format int64
   */
  projectId: number
}

/** 项目模型字段新增输入 */
export interface DevProjectModelFieldAddInput {
  /**
   * 字段名称
   * @minLength 1
   */
  name: string
  /** 字段描述 */
  description?: string | null
  /** 字段类型 */
  dataType?: string | null
  /** 是否必填 */
  isRequired?: string | null
  /**
   * 最大长度
   * @format int32
   */
  maxLength?: number | null
  /**
   * 最小长度
   * @format int32
   */
  minLength?: number | null
  /**
   * 模型Id
   * @format int64
   */
  modelId: number
}

/** 项目模型字段列表查询条件输入 */
export interface DevProjectModelFieldGetListInput {
  /** 字段名称 */
  name?: string | null
  /**
   * 模型Id
   * @format int64
   */
  modelId?: number | null
}

/** 项目模型字段列表查询结果输出 */
export interface DevProjectModelFieldGetListOutput {
  /** @format int64 */
  id?: number
  /** @format date-time */
  createdTime?: string
  createdUserName?: string | null
  modifiedUserName?: string | null
  /** @format date-time */
  modifiedTime?: string | null
  /** 字段名称 */
  name?: string | null
  /** 字段描述 */
  description?: string | null
  /** 字段类型 */
  dataType?: string | null
  /** 是否必填 */
  isRequired?: string | null
  /**
   * 最大长度
   * @format int32
   */
  maxLength?: number | null
  /**
   * 最小长度
   * @format int32
   */
  minLength?: number | null
  /**
   * 模型Id
   * @format int64
   */
  modelId?: number
  /** 模型Id显示文本 */
  modelId_Text?: string | null
}

/** 项目模型字段查询结果输出 */
export interface DevProjectModelFieldGetOutput {
  /** @format int64 */
  id?: number
  /** 字段名称 */
  name?: string | null
  /** 字段描述 */
  description?: string | null
  /** 字段类型 */
  dataType?: string | null
  /** 是否必填 */
  isRequired?: string | null
  /**
   * 最大长度
   * @format int32
   */
  maxLength?: number | null
  /**
   * 最小长度
   * @format int32
   */
  minLength?: number | null
  /**
   * 模型Id
   * @format int64
   */
  modelId?: number
  /** 模型Id显示文本 */
  modelId_Text?: string | null
}

/** 项目模型字段分页查询条件输入 */
export interface DevProjectModelFieldGetPageInput {
  /** 字段名称 */
  name?: string | null
  /**
   * 模型Id
   * @format int64
   */
  modelId?: number | null
}

/** 项目模型字段分页查询结果输出 */
export interface DevProjectModelFieldGetPageOutput {
  /** @format int64 */
  id?: number
  /** @format date-time */
  createdTime?: string
  createdUserName?: string | null
  modifiedUserName?: string | null
  /** @format date-time */
  modifiedTime?: string | null
  /** 字段名称 */
  name?: string | null
  /** 字段描述 */
  description?: string | null
  /** 字段类型 */
  dataType?: string | null
  /** 是否必填 */
  isRequired?: string | null
  /**
   * 最大长度
   * @format int32
   */
  maxLength?: number | null
  /**
   * 最小长度
   * @format int32
   */
  minLength?: number | null
  /**
   * 模型Id
   * @format int64
   */
  modelId?: number
  /** 模型Id显示文本 */
  modelId_Text?: string | null
}

/** 项目模型字段更新数据输入 */
export interface DevProjectModelFieldUpdateInput {
  /** @format int64 */
  id?: number
  /**
   * 字段名称
   * @minLength 1
   */
  name: string
  /** 字段描述 */
  description?: string | null
  /** 字段类型 */
  dataType?: string | null
  /** 是否必填 */
  isRequired?: string | null
  /**
   * 最大长度
   * @format int32
   */
  maxLength?: number | null
  /**
   * 最小长度
   * @format int32
   */
  minLength?: number | null
  /**
   * 模型Id
   * @format int64
   */
  modelId: number
}

/** 项目模型列表查询条件输入 */
export interface DevProjectModelGetListInput {
  /** 模型名称 */
  name?: string | null
  /** 模型编码 */
  code?: string | null
  /**
   * 所属项目
   * @format int64
   */
  projectId?: number | null
}

/** 项目模型列表查询结果输出 */
export interface DevProjectModelGetListOutput {
  /** @format int64 */
  id?: number
  /** @format date-time */
  createdTime?: string
  createdUserName?: string | null
  modifiedUserName?: string | null
  /** @format date-time */
  modifiedTime?: string | null
  /** 模型名称 */
  name?: string | null
  /** 模型编码 */
  code?: string | null
  /** 备注 */
  remark?: string | null
  /**
   * 所属项目
   * @format int64
   */
  projectId?: number
  /** 所属项目显示文本 */
  projectId_Text?: string | null
}

/** 项目模型查询结果输出 */
export interface DevProjectModelGetOutput {
  /** @format int64 */
  id?: number
  /** 模型名称 */
  name?: string | null
  /** 模型编码 */
  code?: string | null
  /** 备注 */
  remark?: string | null
  /**
   * 所属项目
   * @format int64
   */
  projectId?: number
  /** 所属项目显示文本 */
  projectId_Text?: string | null
}

/** 项目模型分页查询条件输入 */
export interface DevProjectModelGetPageInput {
  /** 模型名称 */
  name?: string | null
  /** 模型编码 */
  code?: string | null
  /**
   * 所属项目
   * @format int64
   */
  projectId?: number | null
}

/** 项目模型分页查询结果输出 */
export interface DevProjectModelGetPageOutput {
  /** @format int64 */
  id?: number
  /** @format date-time */
  createdTime?: string
  createdUserName?: string | null
  modifiedUserName?: string | null
  /** @format date-time */
  modifiedTime?: string | null
  /** 模型名称 */
  name?: string | null
  /** 模型编码 */
  code?: string | null
  /** 备注 */
  remark?: string | null
  /**
   * 所属项目
   * @format int64
   */
  projectId?: number
  /** 所属项目显示文本 */
  projectId_Text?: string | null
}

/** 项目模型更新数据输入 */
export interface DevProjectModelUpdateInput {
  /** @format int64 */
  id?: number
  /**
   * 模型名称
   * @minLength 1
   */
  name: string
  /**
   * 模型编码
   * @minLength 1
   */
  code: string
  /** 备注 */
  remark?: string | null
  /**
   * 所属项目
   * @format int64
   */
  projectId: number
}

/** 项目更新数据输入 */
export interface DevProjectUpdateInput {
  /** @format int64 */
  id?: number
  /**
   * 项目名称
   * @minLength 1
   */
  name: string
  /**
   * 项目编码
   * @minLength 1
   */
  code: string
  /** 备注 */
  remark?: string | null
}

/** 模板新增输入 */
export interface DevTemplateAddInput {
  /**
   * 模板名称
   * @minLength 1
   */
  name: string
  /**
   * 模板分组
   * @format int64
   */
  groupId: number
  /** 生成路径 */
  path?: string | null
  /**
   * 模板内容
   * @minLength 1
   */
  content: string
}

/** 模板列表查询条件输入 */
export interface DevTemplateGetListInput {
  /** 模板名称 */
  name?: string | null
  /**
   * 模板分组
   * @format int64
   */
  groupId?: number | null
}

/** 模板列表查询结果输出 */
export interface DevTemplateGetListOutput {
  /** @format int64 */
  id?: number
  /** @format date-time */
  createdTime?: string
  createdUserName?: string | null
  modifiedUserName?: string | null
  /** @format date-time */
  modifiedTime?: string | null
  /** 模板名称 */
  name?: string | null
  /**
   * 模板分组
   * @format int64
   */
  groupId?: number
  /** 模板分组显示文本 */
  groupId_Text?: string | null
  /** 生成路径 */
  path?: string | null
  /** 模板内容 */
  content?: string | null
}

/** 模板查询结果输出 */
export interface DevTemplateGetOutput {
  /** @format int64 */
  id?: number
  /** 模板名称 */
  name?: string | null
  /**
   * 模板分组
   * @format int64
   */
  groupId?: number
  /** 模板分组显示文本 */
  groupId_Text?: string | null
  /** 生成路径 */
  path?: string | null
  /** 模板内容 */
  content?: string | null
}

/** 模板分页查询条件输入 */
export interface DevTemplateGetPageInput {
  /** 模板名称 */
  name?: string | null
  /**
   * 模板分组
   * @format int64
   */
  groupId?: number | null
}

/** 模板分页查询结果输出 */
export interface DevTemplateGetPageOutput {
  /** @format int64 */
  id?: number
  /** @format date-time */
  createdTime?: string
  createdUserName?: string | null
  modifiedUserName?: string | null
  /** @format date-time */
  modifiedTime?: string | null
  /** 模板名称 */
  name?: string | null
  /**
   * 模板分组
   * @format int64
   */
  groupId?: number
  /** 模板分组显示文本 */
  groupId_Text?: string | null
  /** 生成路径 */
  path?: string | null
  /** 模板内容 */
  content?: string | null
}

/** 模板更新数据输入 */
export interface DevTemplateUpdateInput {
  /** @format int64 */
  id?: number
  /**
   * 模板名称
   * @minLength 1
   */
  name: string
  /**
   * 模板分组
   * @format int64
   */
  groupId: number
  /** 生成路径 */
  path?: string | null
  /**
   * 模板内容
   * @minLength 1
   */
  content: string
}

export interface DynamicFilterInfo {
  field?: string | null
  /** Contains=0,StartsWith=1,EndsWith=2,NotContains=3,NotStartsWith=4,NotEndsWith=5,Equal=6,Equals=7,Eq=8,NotEqual=9,GreaterThan=10,GreaterThanOrEqual=11,LessThan=12,LessThanOrEqual=13,Range=14,DateRange=15,Any=16,NotAny=17,Custom=18 */
  operator?: DynamicFilterOperator
  value?: any
  /** And=0,Or=1 */
  logic?: DynamicFilterLogic
  filters?: DynamicFilterInfo[] | null
}

/**
 * And=0,Or=1
 * @format int32
 */
export type DynamicFilterLogic = 0 | 1

/**
 * Contains=0,StartsWith=1,EndsWith=2,NotContains=3,NotStartsWith=4,NotEndsWith=5,Equal=6,Equals=7,Eq=8,NotEqual=9,GreaterThan=10,GreaterThanOrEqual=11,LessThan=12,LessThanOrEqual=13,Range=14,DateRange=15,Any=16,NotAny=17,Custom=18
 * @format int32
 */
export type DynamicFilterOperator = 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12 | 13 | 14 | 15 | 16 | 17 | 18

/** 分页信息输入 */
export interface PageInputDevGroupGetPageInput {
  /**
   * 当前页标
   * @format int32
   */
  currentPage?: number
  /**
   * 每页大小
   * @format int32
   */
  pageSize?: number
  dynamicFilter?: DynamicFilterInfo
  /** 模板组分页查询条件输入 */
  filter?: DevGroupGetPageInput
}

/** 分页信息输入 */
export interface PageInputDevProjectGetPageInput {
  /**
   * 当前页标
   * @format int32
   */
  currentPage?: number
  /**
   * 每页大小
   * @format int32
   */
  pageSize?: number
  dynamicFilter?: DynamicFilterInfo
  /** 项目分页查询条件输入 */
  filter?: DevProjectGetPageInput
}

/** 分页信息输入 */
export interface PageInputDevProjectModelFieldGetPageInput {
  /**
   * 当前页标
   * @format int32
   */
  currentPage?: number
  /**
   * 每页大小
   * @format int32
   */
  pageSize?: number
  dynamicFilter?: DynamicFilterInfo
  /** 项目模型字段分页查询条件输入 */
  filter?: DevProjectModelFieldGetPageInput
}

/** 分页信息输入 */
export interface PageInputDevProjectModelGetPageInput {
  /**
   * 当前页标
   * @format int32
   */
  currentPage?: number
  /**
   * 每页大小
   * @format int32
   */
  pageSize?: number
  dynamicFilter?: DynamicFilterInfo
  /** 项目模型分页查询条件输入 */
  filter?: DevProjectModelGetPageInput
}

/** 分页信息输入 */
export interface PageInputDevTemplateGetPageInput {
  /**
   * 当前页标
   * @format int32
   */
  currentPage?: number
  /**
   * 每页大小
   * @format int32
   */
  pageSize?: number
  dynamicFilter?: DynamicFilterInfo
  /** 模板分页查询条件输入 */
  filter?: DevTemplateGetPageInput
}

/** 分页信息输出 */
export interface PageOutputDevGroupGetPageOutput {
  /**
   * 数据总数
   * @format int64
   */
  total?: number
  /** 数据 */
  list?: DevGroupGetPageOutput[] | null
}

/** 分页信息输出 */
export interface PageOutputDevProjectGetPageOutput {
  /**
   * 数据总数
   * @format int64
   */
  total?: number
  /** 数据 */
  list?: DevProjectGetPageOutput[] | null
}

/** 分页信息输出 */
export interface PageOutputDevProjectModelFieldGetPageOutput {
  /**
   * 数据总数
   * @format int64
   */
  total?: number
  /** 数据 */
  list?: DevProjectModelFieldGetPageOutput[] | null
}

/** 分页信息输出 */
export interface PageOutputDevProjectModelGetPageOutput {
  /**
   * 数据总数
   * @format int64
   */
  total?: number
  /** 数据 */
  list?: DevProjectModelGetPageOutput[] | null
}

/** 分页信息输出 */
export interface PageOutputDevTemplateGetPageOutput {
  /**
   * 数据总数
   * @format int64
   */
  total?: number
  /** 数据 */
  list?: DevTemplateGetPageOutput[] | null
}

/** 结果输出 */
export interface ResultOutputBoolean {
  /** 是否成功标记 */
  success?: boolean
  /** 编码 */
  code?: string | null
  /** 消息 */
  msg?: string | null
  /** 数据 */
  data?: boolean
}

/** 结果输出 */
export interface ResultOutputDevGroupGetOutput {
  /** 是否成功标记 */
  success?: boolean
  /** 编码 */
  code?: string | null
  /** 消息 */
  msg?: string | null
  /** 模板组查询结果输出 */
  data?: DevGroupGetOutput
}

/** 结果输出 */
export interface ResultOutputDevProjectGetOutput {
  /** 是否成功标记 */
  success?: boolean
  /** 编码 */
  code?: string | null
  /** 消息 */
  msg?: string | null
  /** 项目查询结果输出 */
  data?: DevProjectGetOutput
}

/** 结果输出 */
export interface ResultOutputDevProjectModelFieldGetOutput {
  /** 是否成功标记 */
  success?: boolean
  /** 编码 */
  code?: string | null
  /** 消息 */
  msg?: string | null
  /** 项目模型字段查询结果输出 */
  data?: DevProjectModelFieldGetOutput
}

/** 结果输出 */
export interface ResultOutputDevProjectModelGetOutput {
  /** 是否成功标记 */
  success?: boolean
  /** 编码 */
  code?: string | null
  /** 消息 */
  msg?: string | null
  /** 项目模型查询结果输出 */
  data?: DevProjectModelGetOutput
}

/** 结果输出 */
export interface ResultOutputDevTemplateGetOutput {
  /** 是否成功标记 */
  success?: boolean
  /** 编码 */
  code?: string | null
  /** 消息 */
  msg?: string | null
  /** 模板查询结果输出 */
  data?: DevTemplateGetOutput
}

/** 结果输出 */
export interface ResultOutputIEnumerableDevGroupGetListOutput {
  /** 是否成功标记 */
  success?: boolean
  /** 编码 */
  code?: string | null
  /** 消息 */
  msg?: string | null
  /** 数据 */
  data?: DevGroupGetListOutput[] | null
}

/** 结果输出 */
export interface ResultOutputIEnumerableDevProjectGetListOutput {
  /** 是否成功标记 */
  success?: boolean
  /** 编码 */
  code?: string | null
  /** 消息 */
  msg?: string | null
  /** 数据 */
  data?: DevProjectGetListOutput[] | null
}

/** 结果输出 */
export interface ResultOutputIEnumerableDevProjectModelFieldGetListOutput {
  /** 是否成功标记 */
  success?: boolean
  /** 编码 */
  code?: string | null
  /** 消息 */
  msg?: string | null
  /** 数据 */
  data?: DevProjectModelFieldGetListOutput[] | null
}

/** 结果输出 */
export interface ResultOutputIEnumerableDevProjectModelGetListOutput {
  /** 是否成功标记 */
  success?: boolean
  /** 编码 */
  code?: string | null
  /** 消息 */
  msg?: string | null
  /** 数据 */
  data?: DevProjectModelGetListOutput[] | null
}

/** 结果输出 */
export interface ResultOutputIEnumerableDevTemplateGetListOutput {
  /** 是否成功标记 */
  success?: boolean
  /** 编码 */
  code?: string | null
  /** 消息 */
  msg?: string | null
  /** 数据 */
  data?: DevTemplateGetListOutput[] | null
}

/** 结果输出 */
export interface ResultOutputInt64 {
  /** 是否成功标记 */
  success?: boolean
  /** 编码 */
  code?: string | null
  /** 消息 */
  msg?: string | null
  /**
   * 数据
   * @format int64
   */
  data?: number
}

/** 结果输出 */
export interface ResultOutputPageOutputDevGroupGetPageOutput {
  /** 是否成功标记 */
  success?: boolean
  /** 编码 */
  code?: string | null
  /** 消息 */
  msg?: string | null
  /** 分页信息输出 */
  data?: PageOutputDevGroupGetPageOutput
}

/** 结果输出 */
export interface ResultOutputPageOutputDevProjectGetPageOutput {
  /** 是否成功标记 */
  success?: boolean
  /** 编码 */
  code?: string | null
  /** 消息 */
  msg?: string | null
  /** 分页信息输出 */
  data?: PageOutputDevProjectGetPageOutput
}

/** 结果输出 */
export interface ResultOutputPageOutputDevProjectModelFieldGetPageOutput {
  /** 是否成功标记 */
  success?: boolean
  /** 编码 */
  code?: string | null
  /** 消息 */
  msg?: string | null
  /** 分页信息输出 */
  data?: PageOutputDevProjectModelFieldGetPageOutput
}

/** 结果输出 */
export interface ResultOutputPageOutputDevProjectModelGetPageOutput {
  /** 是否成功标记 */
  success?: boolean
  /** 编码 */
  code?: string | null
  /** 消息 */
  msg?: string | null
  /** 分页信息输出 */
  data?: PageOutputDevProjectModelGetPageOutput
}

/** 结果输出 */
export interface ResultOutputPageOutputDevTemplateGetPageOutput {
  /** 是否成功标记 */
  success?: boolean
  /** 编码 */
  code?: string | null
  /** 消息 */
  msg?: string | null
  /** 分页信息输出 */
  data?: PageOutputDevTemplateGetPageOutput
}
