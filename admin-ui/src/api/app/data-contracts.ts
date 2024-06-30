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

/** 添加模块 */
export interface ModuleAddInput {
  /** 名称 */
  name?: string | null
}

/** 模块 */
export interface ModuleGetOutput {
  /** 名称 */
  name?: string | null
  /**
   * 编号
   * @format int64
   */
  id: number
  /**
   * 版本
   * @format int64
   */
  version?: number
}

/** 模块分页 */
export interface ModuleGetPageDto {
  /** 名称 */
  name?: string | null
}

/** 模块列表 */
export interface ModuleListOutput {
  /**
   * 主键
   * @format int64
   */
  id?: number
  /** 名称 */
  name?: string | null
}

/** 修改模块 */
export interface ModuleUpdateInput {
  /** 名称 */
  name?: string | null
  /**
   * 编号
   * @format int64
   */
  id: number
  /**
   * 版本
   * @format int64
   */
  version?: number
}

/** 分页信息输入 */
export interface PageInputModuleGetPageDto {
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
  /** 模块分页 */
  filter?: ModuleGetPageDto
}

/** 分页信息输出 */
export interface PageOutputModuleListOutput {
  /**
   * 数据总数
   * @format int64
   */
  total?: number
  /** 数据 */
  list?: ModuleListOutput[] | null
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
export interface ResultOutputModuleGetOutput {
  /** 是否成功标记 */
  success?: boolean
  /** 编码 */
  code?: string | null
  /** 消息 */
  msg?: string | null
  /** 模块 */
  data?: ModuleGetOutput
}

/** 结果输出 */
export interface ResultOutputPageOutputModuleListOutput {
  /** 是否成功标记 */
  success?: boolean
  /** 编码 */
  code?: string | null
  /** 消息 */
  msg?: string | null
  /** 分页信息输出 */
  data?: PageOutputModuleListOutput
}
