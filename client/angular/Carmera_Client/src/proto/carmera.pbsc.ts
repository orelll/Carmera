/* tslint:disable */
/* eslint-disable */
// @ts-nocheck
//
// THIS IS A GENERATED FILE
// DO NOT MODIFY IT! YOUR CHANGES WILL BE LOST
import { Inject, Injectable, Optional } from '@angular/core';
import {
  GrpcCallType,
  GrpcClient,
  GrpcClientFactory,
  GrpcEvent,
  GrpcMetadata
} from '@ngx-grpc/common';
import {
  GRPC_CLIENT_FACTORY,
  GrpcHandler,
  takeMessages,
  throwStatusErrors
} from '@ngx-grpc/core';
import { Observable } from 'rxjs';
import * as thisProto from './carmera.pb';
import { GRPC_CARMERA_LOADER_CLIENT_SETTINGS } from './carmera.pbconf';
/**
 * Service client implementation for greet.CarmeraLoader
 */
@Injectable({ providedIn: 'any' })
export class CarmeraLoaderClient {
  private client: GrpcClient<any>;

  /**
   * Raw RPC implementation for each service client method.
   * The raw methods provide more control on the incoming data and events. E.g. they can be useful to read status `OK` metadata.
   * Attention: these methods do not throw errors when non-zero status codes are received.
   */
  $raw = {
    /**
     * Unary call: /greet.CarmeraLoader/HealthCheck
     *
     * @param requestMessage Request message
     * @param requestMetadata Request metadata
     * @returns Observable<GrpcEvent<thisProto.BooleanReply>>
     */
    healthCheck: (
      requestData: thisProto.EmptyRequest,
      requestMetadata = new GrpcMetadata()
    ): Observable<GrpcEvent<thisProto.BooleanReply>> => {
      return this.handler.handle({
        type: GrpcCallType.unary,
        client: this.client,
        path: '/greet.CarmeraLoader/HealthCheck',
        requestData,
        requestMetadata,
        requestClass: thisProto.EmptyRequest,
        responseClass: thisProto.BooleanReply
      });
    },
    /**
     * Unary call: /greet.CarmeraLoader/CurrentLocations
     *
     * @param requestMessage Request message
     * @param requestMetadata Request metadata
     * @returns Observable<GrpcEvent<thisProto.CurrentLocationsReply>>
     */
    currentLocations: (
      requestData: thisProto.EmptyRequest,
      requestMetadata = new GrpcMetadata()
    ): Observable<GrpcEvent<thisProto.CurrentLocationsReply>> => {
      return this.handler.handle({
        type: GrpcCallType.unary,
        client: this.client,
        path: '/greet.CarmeraLoader/CurrentLocations',
        requestData,
        requestMetadata,
        requestClass: thisProto.EmptyRequest,
        responseClass: thisProto.CurrentLocationsReply
      });
    },
    /**
     * Unary call: /greet.CarmeraLoader/ListCameras
     *
     * @param requestMessage Request message
     * @param requestMetadata Request metadata
     * @returns Observable<GrpcEvent<thisProto.ListCamerasReply>>
     */
    listCameras: (
      requestData: thisProto.EmptyRequest,
      requestMetadata = new GrpcMetadata()
    ): Observable<GrpcEvent<thisProto.ListCamerasReply>> => {
      return this.handler.handle({
        type: GrpcCallType.unary,
        client: this.client,
        path: '/greet.CarmeraLoader/ListCameras',
        requestData,
        requestMetadata,
        requestClass: thisProto.EmptyRequest,
        responseClass: thisProto.ListCamerasReply
      });
    },
    /**
     * Unary call: /greet.CarmeraLoader/CheckConfiguration
     *
     * @param requestMessage Request message
     * @param requestMetadata Request metadata
     * @returns Observable<GrpcEvent<thisProto.BooleanReply>>
     */
    checkConfiguration: (
      requestData: thisProto.EmptyRequest,
      requestMetadata = new GrpcMetadata()
    ): Observable<GrpcEvent<thisProto.BooleanReply>> => {
      return this.handler.handle({
        type: GrpcCallType.unary,
        client: this.client,
        path: '/greet.CarmeraLoader/CheckConfiguration',
        requestData,
        requestMetadata,
        requestClass: thisProto.EmptyRequest,
        responseClass: thisProto.BooleanReply
      });
    },
    /**
     * Unary call: /greet.CarmeraLoader/GetPhoto
     *
     * @param requestMessage Request message
     * @param requestMetadata Request metadata
     * @returns Observable<GrpcEvent<thisProto.GetPhotoReply>>
     */
    getPhoto: (
      requestData: thisProto.GetPhotoRequest,
      requestMetadata = new GrpcMetadata()
    ): Observable<GrpcEvent<thisProto.GetPhotoReply>> => {
      return this.handler.handle({
        type: GrpcCallType.unary,
        client: this.client,
        path: '/greet.CarmeraLoader/GetPhoto',
        requestData,
        requestMetadata,
        requestClass: thisProto.GetPhotoRequest,
        responseClass: thisProto.GetPhotoReply
      });
    },
    /**
     * Server streaming: /greet.CarmeraLoader/TestStream
     *
     * @param requestMessage Request message
     * @param requestMetadata Request metadata
     * @returns Observable<GrpcEvent<thisProto.BooleanReply>>
     */
    testStream: (
      requestData: thisProto.EmptyRequest,
      requestMetadata = new GrpcMetadata()
    ): Observable<GrpcEvent<thisProto.BooleanReply>> => {
      return this.handler.handle({
        type: GrpcCallType.serverStream,
        client: this.client,
        path: '/greet.CarmeraLoader/TestStream',
        requestData,
        requestMetadata,
        requestClass: thisProto.EmptyRequest,
        responseClass: thisProto.BooleanReply
      });
    }
  };

  constructor(
    @Optional() @Inject(GRPC_CARMERA_LOADER_CLIENT_SETTINGS) settings: any,
    @Inject(GRPC_CLIENT_FACTORY) clientFactory: GrpcClientFactory<any>,
    private handler: GrpcHandler
  ) {
    this.client = clientFactory.createClient('greet.CarmeraLoader', settings);
  }

  /**
   * Unary call @/greet.CarmeraLoader/HealthCheck
   *
   * @param requestMessage Request message
   * @param requestMetadata Request metadata
   * @returns Observable<thisProto.BooleanReply>
   */
  healthCheck(
    requestData: thisProto.EmptyRequest,
    requestMetadata = new GrpcMetadata()
  ): Observable<thisProto.BooleanReply> {
    return this.$raw
      .healthCheck(requestData, requestMetadata)
      .pipe(throwStatusErrors(), takeMessages());
  }

  /**
   * Unary call @/greet.CarmeraLoader/CurrentLocations
   *
   * @param requestMessage Request message
   * @param requestMetadata Request metadata
   * @returns Observable<thisProto.CurrentLocationsReply>
   */
  currentLocations(
    requestData: thisProto.EmptyRequest,
    requestMetadata = new GrpcMetadata()
  ): Observable<thisProto.CurrentLocationsReply> {
    return this.$raw
      .currentLocations(requestData, requestMetadata)
      .pipe(throwStatusErrors(), takeMessages());
  }

  /**
   * Unary call @/greet.CarmeraLoader/ListCameras
   *
   * @param requestMessage Request message
   * @param requestMetadata Request metadata
   * @returns Observable<thisProto.ListCamerasReply>
   */
  listCameras(
    requestData: thisProto.EmptyRequest,
    requestMetadata = new GrpcMetadata()
  ): Observable<thisProto.ListCamerasReply> {
    return this.$raw
      .listCameras(requestData, requestMetadata)
      .pipe(throwStatusErrors(), takeMessages());
  }

  /**
   * Unary call @/greet.CarmeraLoader/CheckConfiguration
   *
   * @param requestMessage Request message
   * @param requestMetadata Request metadata
   * @returns Observable<thisProto.BooleanReply>
   */
  checkConfiguration(
    requestData: thisProto.EmptyRequest,
    requestMetadata = new GrpcMetadata()
  ): Observable<thisProto.BooleanReply> {
    return this.$raw
      .checkConfiguration(requestData, requestMetadata)
      .pipe(throwStatusErrors(), takeMessages());
  }

  /**
   * Unary call @/greet.CarmeraLoader/GetPhoto
   *
   * @param requestMessage Request message
   * @param requestMetadata Request metadata
   * @returns Observable<thisProto.GetPhotoReply>
   */
  getPhoto(
    requestData: thisProto.GetPhotoRequest,
    requestMetadata = new GrpcMetadata()
  ): Observable<thisProto.GetPhotoReply> {
    return this.$raw
      .getPhoto(requestData, requestMetadata)
      .pipe(throwStatusErrors(), takeMessages());
  }

  /**
   * Server streaming @/greet.CarmeraLoader/TestStream
   *
   * @param requestMessage Request message
   * @param requestMetadata Request metadata
   * @returns Observable<thisProto.BooleanReply>
   */
  testStream(
    requestData: thisProto.EmptyRequest,
    requestMetadata = new GrpcMetadata()
  ): Observable<thisProto.BooleanReply> {
    return this.$raw
      .testStream(requestData, requestMetadata)
      .pipe(throwStatusErrors(), takeMessages());
  }
}
