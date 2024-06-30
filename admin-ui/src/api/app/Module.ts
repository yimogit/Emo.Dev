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

import { AxiosResponse } from 'axios'
import {
  ModuleAddInput,
  ModuleUpdateInput,
  PageInputModuleGetPageDto,
  ResultOutputInt64,
  ResultOutputModuleGetOutput,
  ResultOutputPageOutputModuleListOutput,
} from './data-contracts'
import { ContentType, HttpClient, RequestParams } from './http-client'

export class ModuleApi<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags module
   * @name Get
   * @summary 查询模块
   * @request GET:/api/app/module/get
   * @secure
   */
  get = (
    query?: {
      /** @format int64 */
      id?: number
    },
    params: RequestParams = {}
  ) =>
    this.request<ResultOutputModuleGetOutput, any>({
      path: `/api/app/module/get`,
      method: 'GET',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags module
   * @name GetPage
   * @summary 查询分页
   * @request POST:/api/app/module/get-page
   * @secure
   */
  getPage = (data: PageInputModuleGetPageDto, params: RequestParams = {}) =>
    this.request<ResultOutputPageOutputModuleListOutput, any>({
      path: `/api/app/module/get-page`,
      method: 'POST',
      body: data,
      secure: true,
      type: ContentType.Json,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags module
   * @name Add
   * @summary 新增
   * @request POST:/api/app/module/add
   * @secure
   */
  add = (data: ModuleAddInput, params: RequestParams = {}) =>
    this.request<ResultOutputInt64, any>({
      path: `/api/app/module/add`,
      method: 'POST',
      body: data,
      secure: true,
      type: ContentType.Json,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags module
   * @name Update
   * @summary 修改
   * @request PUT:/api/app/module/update
   * @secure
   */
  update = (data: ModuleUpdateInput, params: RequestParams = {}) =>
    this.request<AxiosResponse, any>({
      path: `/api/app/module/update`,
      method: 'PUT',
      body: data,
      secure: true,
      type: ContentType.Json,
      ...params,
    })
  /**
   * No description
   *
   * @tags module
   * @name Delete
   * @summary 彻底删除
   * @request DELETE:/api/app/module/delete
   * @secure
   */
  delete = (
    query?: {
      /** @format int64 */
      id?: number
    },
    params: RequestParams = {}
  ) =>
    this.request<AxiosResponse, any>({
      path: `/api/app/module/delete`,
      method: 'DELETE',
      query: query,
      secure: true,
      ...params,
    })
  /**
   * No description
   *
   * @tags module
   * @name SoftDelete
   * @summary 删除
   * @request DELETE:/api/app/module/soft-delete
   * @secure
   */
  softDelete = (
    query?: {
      /** @format int64 */
      id?: number
    },
    params: RequestParams = {}
  ) =>
    this.request<AxiosResponse, any>({
      path: `/api/app/module/soft-delete`,
      method: 'DELETE',
      query: query,
      secure: true,
      ...params,
    })
  /**
   * No description
   *
   * @tags module
   * @name BatchSoftDelete
   * @summary 批量删除
   * @request PUT:/api/app/module/batch-soft-delete
   * @secure
   */
  batchSoftDelete = (data: number[], params: RequestParams = {}) =>
    this.request<AxiosResponse, any>({
      path: `/api/app/module/batch-soft-delete`,
      method: 'PUT',
      body: data,
      secure: true,
      type: ContentType.Json,
      ...params,
    })
  /**
   * No description
   *
   * @tags module
   * @name ExecuteTask
   * @summary 执行任务
   * @request POST:/api/app/module/execute-task
   * @secure
   */
  executeTask = (params: RequestParams = {}) =>
    this.request<AxiosResponse, any>({
      path: `/api/app/module/execute-task`,
      method: 'POST',
      secure: true,
      ...params,
    })
}
