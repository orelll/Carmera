/* tslint:disable */
/* eslint-disable */
// @ts-nocheck
//
// THIS IS A GENERATED FILE
// DO NOT MODIFY IT! YOUR CHANGES WILL BE LOST
import {
  GrpcMessage,
  RecursivePartial,
  ToProtobufJSONOptions
} from '@ngx-grpc/common';
import { BinaryReader, BinaryWriter, ByteSource } from 'google-protobuf';

/**
 * Message implementation for greet.EmptyRequest
 */
export class EmptyRequest implements GrpcMessage {
  static id = 'greet.EmptyRequest';

  /**
   * Deserialize binary data to message
   * @param instance message instance
   */
  static deserializeBinary(bytes: ByteSource) {
    const instance = new EmptyRequest();
    EmptyRequest.deserializeBinaryFromReader(instance, new BinaryReader(bytes));
    return instance;
  }

  /**
   * Check all the properties and set default protobuf values if necessary
   * @param _instance message instance
   */
  static refineValues(_instance: EmptyRequest) {}

  /**
   * Deserializes / reads binary message into message instance using provided binary reader
   * @param _instance message instance
   * @param _reader binary reader instance
   */
  static deserializeBinaryFromReader(
    _instance: EmptyRequest,
    _reader: BinaryReader
  ) {
    while (_reader.nextField()) {
      if (_reader.isEndGroup()) break;

      switch (_reader.getFieldNumber()) {
        default:
          _reader.skipField();
      }
    }

    EmptyRequest.refineValues(_instance);
  }

  /**
   * Serializes a message to binary format using provided binary reader
   * @param _instance message instance
   * @param _writer binary writer instance
   */
  static serializeBinaryToWriter(
    _instance: EmptyRequest,
    _writer: BinaryWriter
  ) {}

  /**
   * Message constructor. Initializes the properties and applies default Protobuf values if necessary
   * @param _value initial values object or instance of EmptyRequest to deeply clone from
   */
  constructor(_value?: RecursivePartial<EmptyRequest.AsObject>) {
    _value = _value || {};
    EmptyRequest.refineValues(this);
  }

  /**
   * Serialize message to binary data
   * @param instance message instance
   */
  serializeBinary() {
    const writer = new BinaryWriter();
    EmptyRequest.serializeBinaryToWriter(this, writer);
    return writer.getResultBuffer();
  }

  /**
   * Cast message to standard JavaScript object (all non-primitive values are deeply cloned)
   */
  toObject(): EmptyRequest.AsObject {
    return {};
  }

  /**
   * Convenience method to support JSON.stringify(message), replicates the structure of toObject()
   */
  toJSON() {
    return this.toObject();
  }

  /**
   * Cast message to JSON using protobuf JSON notation: https://developers.google.com/protocol-buffers/docs/proto3#json
   * Attention: output differs from toObject() e.g. enums are represented as names and not as numbers, Timestamp is an ISO Date string format etc.
   * If the message itself or some of descendant messages is google.protobuf.Any, you MUST provide a message pool as options. If not, the messagePool is not required
   */
  toProtobufJSON(
    // @ts-ignore
    options?: ToProtobufJSONOptions
  ): EmptyRequest.AsProtobufJSON {
    return {};
  }
}
export module EmptyRequest {
  /**
   * Standard JavaScript object representation for EmptyRequest
   */
  export interface AsObject {}

  /**
   * Protobuf JSON representation for EmptyRequest
   */
  export interface AsProtobufJSON {}
}

/**
 * Message implementation for greet.BooleanReply
 */
export class BooleanReply implements GrpcMessage {
  static id = 'greet.BooleanReply';

  /**
   * Deserialize binary data to message
   * @param instance message instance
   */
  static deserializeBinary(bytes: ByteSource) {
    const instance = new BooleanReply();
    BooleanReply.deserializeBinaryFromReader(instance, new BinaryReader(bytes));
    return instance;
  }

  /**
   * Check all the properties and set default protobuf values if necessary
   * @param _instance message instance
   */
  static refineValues(_instance: BooleanReply) {
    _instance.value = _instance.value || false;
    _instance.message = _instance.message || '';
  }

  /**
   * Deserializes / reads binary message into message instance using provided binary reader
   * @param _instance message instance
   * @param _reader binary reader instance
   */
  static deserializeBinaryFromReader(
    _instance: BooleanReply,
    _reader: BinaryReader
  ) {
    while (_reader.nextField()) {
      if (_reader.isEndGroup()) break;

      switch (_reader.getFieldNumber()) {
        case 1:
          _instance.value = _reader.readBool();
          break;
        case 2:
          _instance.message = _reader.readString();
          break;
        default:
          _reader.skipField();
      }
    }

    BooleanReply.refineValues(_instance);
  }

  /**
   * Serializes a message to binary format using provided binary reader
   * @param _instance message instance
   * @param _writer binary writer instance
   */
  static serializeBinaryToWriter(
    _instance: BooleanReply,
    _writer: BinaryWriter
  ) {
    if (_instance.value) {
      _writer.writeBool(1, _instance.value);
    }
    if (_instance.message) {
      _writer.writeString(2, _instance.message);
    }
  }

  private _value: boolean;
  private _message: string;

  /**
   * Message constructor. Initializes the properties and applies default Protobuf values if necessary
   * @param _value initial values object or instance of BooleanReply to deeply clone from
   */
  constructor(_value?: RecursivePartial<BooleanReply.AsObject>) {
    _value = _value || {};
    this.value = _value.value;
    this.message = _value.message;
    BooleanReply.refineValues(this);
  }
  get value(): boolean {
    return this._value;
  }
  set value(value: boolean) {
    this._value = value;
  }
  get message(): string {
    return this._message;
  }
  set message(value: string) {
    this._message = value;
  }

  /**
   * Serialize message to binary data
   * @param instance message instance
   */
  serializeBinary() {
    const writer = new BinaryWriter();
    BooleanReply.serializeBinaryToWriter(this, writer);
    return writer.getResultBuffer();
  }

  /**
   * Cast message to standard JavaScript object (all non-primitive values are deeply cloned)
   */
  toObject(): BooleanReply.AsObject {
    return {
      value: this.value,
      message: this.message
    };
  }

  /**
   * Convenience method to support JSON.stringify(message), replicates the structure of toObject()
   */
  toJSON() {
    return this.toObject();
  }

  /**
   * Cast message to JSON using protobuf JSON notation: https://developers.google.com/protocol-buffers/docs/proto3#json
   * Attention: output differs from toObject() e.g. enums are represented as names and not as numbers, Timestamp is an ISO Date string format etc.
   * If the message itself or some of descendant messages is google.protobuf.Any, you MUST provide a message pool as options. If not, the messagePool is not required
   */
  toProtobufJSON(
    // @ts-ignore
    options?: ToProtobufJSONOptions
  ): BooleanReply.AsProtobufJSON {
    return {
      value: this.value,
      message: this.message
    };
  }
}
export module BooleanReply {
  /**
   * Standard JavaScript object representation for BooleanReply
   */
  export interface AsObject {
    value: boolean;
    message: string;
  }

  /**
   * Protobuf JSON representation for BooleanReply
   */
  export interface AsProtobufJSON {
    value: boolean;
    message: string;
  }
}

/**
 * Message implementation for greet.CurrentLocationsReply
 */
export class CurrentLocationsReply implements GrpcMessage {
  static id = 'greet.CurrentLocationsReply';

  /**
   * Deserialize binary data to message
   * @param instance message instance
   */
  static deserializeBinary(bytes: ByteSource) {
    const instance = new CurrentLocationsReply();
    CurrentLocationsReply.deserializeBinaryFromReader(
      instance,
      new BinaryReader(bytes)
    );
    return instance;
  }

  /**
   * Check all the properties and set default protobuf values if necessary
   * @param _instance message instance
   */
  static refineValues(_instance: CurrentLocationsReply) {
    _instance.locations = _instance.locations || [];
  }

  /**
   * Deserializes / reads binary message into message instance using provided binary reader
   * @param _instance message instance
   * @param _reader binary reader instance
   */
  static deserializeBinaryFromReader(
    _instance: CurrentLocationsReply,
    _reader: BinaryReader
  ) {
    while (_reader.nextField()) {
      if (_reader.isEndGroup()) break;

      switch (_reader.getFieldNumber()) {
        case 1:
          (_instance.locations = _instance.locations || []).push(
            _reader.readString()
          );
          break;
        default:
          _reader.skipField();
      }
    }

    CurrentLocationsReply.refineValues(_instance);
  }

  /**
   * Serializes a message to binary format using provided binary reader
   * @param _instance message instance
   * @param _writer binary writer instance
   */
  static serializeBinaryToWriter(
    _instance: CurrentLocationsReply,
    _writer: BinaryWriter
  ) {
    if (_instance.locations && _instance.locations.length) {
      _writer.writeRepeatedString(1, _instance.locations);
    }
  }

  private _locations: string[];

  /**
   * Message constructor. Initializes the properties and applies default Protobuf values if necessary
   * @param _value initial values object or instance of CurrentLocationsReply to deeply clone from
   */
  constructor(_value?: RecursivePartial<CurrentLocationsReply.AsObject>) {
    _value = _value || {};
    this.locations = (_value.locations || []).slice();
    CurrentLocationsReply.refineValues(this);
  }
  get locations(): string[] {
    return this._locations;
  }
  set locations(value: string[]) {
    this._locations = value;
  }

  /**
   * Serialize message to binary data
   * @param instance message instance
   */
  serializeBinary() {
    const writer = new BinaryWriter();
    CurrentLocationsReply.serializeBinaryToWriter(this, writer);
    return writer.getResultBuffer();
  }

  /**
   * Cast message to standard JavaScript object (all non-primitive values are deeply cloned)
   */
  toObject(): CurrentLocationsReply.AsObject {
    return {
      locations: (this.locations || []).slice()
    };
  }

  /**
   * Convenience method to support JSON.stringify(message), replicates the structure of toObject()
   */
  toJSON() {
    return this.toObject();
  }

  /**
   * Cast message to JSON using protobuf JSON notation: https://developers.google.com/protocol-buffers/docs/proto3#json
   * Attention: output differs from toObject() e.g. enums are represented as names and not as numbers, Timestamp is an ISO Date string format etc.
   * If the message itself or some of descendant messages is google.protobuf.Any, you MUST provide a message pool as options. If not, the messagePool is not required
   */
  toProtobufJSON(
    // @ts-ignore
    options?: ToProtobufJSONOptions
  ): CurrentLocationsReply.AsProtobufJSON {
    return {
      locations: (this.locations || []).slice()
    };
  }
}
export module CurrentLocationsReply {
  /**
   * Standard JavaScript object representation for CurrentLocationsReply
   */
  export interface AsObject {
    locations: string[];
  }

  /**
   * Protobuf JSON representation for CurrentLocationsReply
   */
  export interface AsProtobufJSON {
    locations: string[];
  }
}

/**
 * Message implementation for greet.ListCamerasReply
 */
export class ListCamerasReply implements GrpcMessage {
  static id = 'greet.ListCamerasReply';

  /**
   * Deserialize binary data to message
   * @param instance message instance
   */
  static deserializeBinary(bytes: ByteSource) {
    const instance = new ListCamerasReply();
    ListCamerasReply.deserializeBinaryFromReader(
      instance,
      new BinaryReader(bytes)
    );
    return instance;
  }

  /**
   * Check all the properties and set default protobuf values if necessary
   * @param _instance message instance
   */
  static refineValues(_instance: ListCamerasReply) {
    _instance.cameras = _instance.cameras || [];
  }

  /**
   * Deserializes / reads binary message into message instance using provided binary reader
   * @param _instance message instance
   * @param _reader binary reader instance
   */
  static deserializeBinaryFromReader(
    _instance: ListCamerasReply,
    _reader: BinaryReader
  ) {
    while (_reader.nextField()) {
      if (_reader.isEndGroup()) break;

      switch (_reader.getFieldNumber()) {
        case 1:
          (_instance.cameras = _instance.cameras || []).push(
            _reader.readString()
          );
          break;
        default:
          _reader.skipField();
      }
    }

    ListCamerasReply.refineValues(_instance);
  }

  /**
   * Serializes a message to binary format using provided binary reader
   * @param _instance message instance
   * @param _writer binary writer instance
   */
  static serializeBinaryToWriter(
    _instance: ListCamerasReply,
    _writer: BinaryWriter
  ) {
    if (_instance.cameras && _instance.cameras.length) {
      _writer.writeRepeatedString(1, _instance.cameras);
    }
  }

  private _cameras: string[];

  /**
   * Message constructor. Initializes the properties and applies default Protobuf values if necessary
   * @param _value initial values object or instance of ListCamerasReply to deeply clone from
   */
  constructor(_value?: RecursivePartial<ListCamerasReply.AsObject>) {
    _value = _value || {};
    this.cameras = (_value.cameras || []).slice();
    ListCamerasReply.refineValues(this);
  }
  get cameras(): string[] {
    return this._cameras;
  }
  set cameras(value: string[]) {
    this._cameras = value;
  }

  /**
   * Serialize message to binary data
   * @param instance message instance
   */
  serializeBinary() {
    const writer = new BinaryWriter();
    ListCamerasReply.serializeBinaryToWriter(this, writer);
    return writer.getResultBuffer();
  }

  /**
   * Cast message to standard JavaScript object (all non-primitive values are deeply cloned)
   */
  toObject(): ListCamerasReply.AsObject {
    return {
      cameras: (this.cameras || []).slice()
    };
  }

  /**
   * Convenience method to support JSON.stringify(message), replicates the structure of toObject()
   */
  toJSON() {
    return this.toObject();
  }

  /**
   * Cast message to JSON using protobuf JSON notation: https://developers.google.com/protocol-buffers/docs/proto3#json
   * Attention: output differs from toObject() e.g. enums are represented as names and not as numbers, Timestamp is an ISO Date string format etc.
   * If the message itself or some of descendant messages is google.protobuf.Any, you MUST provide a message pool as options. If not, the messagePool is not required
   */
  toProtobufJSON(
    // @ts-ignore
    options?: ToProtobufJSONOptions
  ): ListCamerasReply.AsProtobufJSON {
    return {
      cameras: (this.cameras || []).slice()
    };
  }
}
export module ListCamerasReply {
  /**
   * Standard JavaScript object representation for ListCamerasReply
   */
  export interface AsObject {
    cameras: string[];
  }

  /**
   * Protobuf JSON representation for ListCamerasReply
   */
  export interface AsProtobufJSON {
    cameras: string[];
  }
}

/**
 * Message implementation for greet.GetPhotoRequest
 */
export class GetPhotoRequest implements GrpcMessage {
  static id = 'greet.GetPhotoRequest';

  /**
   * Deserialize binary data to message
   * @param instance message instance
   */
  static deserializeBinary(bytes: ByteSource) {
    const instance = new GetPhotoRequest();
    GetPhotoRequest.deserializeBinaryFromReader(
      instance,
      new BinaryReader(bytes)
    );
    return instance;
  }

  /**
   * Check all the properties and set default protobuf values if necessary
   * @param _instance message instance
   */
  static refineValues(_instance: GetPhotoRequest) {
    _instance.cameraName = _instance.cameraName || '';
  }

  /**
   * Deserializes / reads binary message into message instance using provided binary reader
   * @param _instance message instance
   * @param _reader binary reader instance
   */
  static deserializeBinaryFromReader(
    _instance: GetPhotoRequest,
    _reader: BinaryReader
  ) {
    while (_reader.nextField()) {
      if (_reader.isEndGroup()) break;

      switch (_reader.getFieldNumber()) {
        case 1:
          _instance.cameraName = _reader.readString();
          break;
        default:
          _reader.skipField();
      }
    }

    GetPhotoRequest.refineValues(_instance);
  }

  /**
   * Serializes a message to binary format using provided binary reader
   * @param _instance message instance
   * @param _writer binary writer instance
   */
  static serializeBinaryToWriter(
    _instance: GetPhotoRequest,
    _writer: BinaryWriter
  ) {
    if (_instance.cameraName) {
      _writer.writeString(1, _instance.cameraName);
    }
  }

  private _cameraName: string;

  /**
   * Message constructor. Initializes the properties and applies default Protobuf values if necessary
   * @param _value initial values object or instance of GetPhotoRequest to deeply clone from
   */
  constructor(_value?: RecursivePartial<GetPhotoRequest.AsObject>) {
    _value = _value || {};
    this.cameraName = _value.cameraName;
    GetPhotoRequest.refineValues(this);
  }
  get cameraName(): string {
    return this._cameraName;
  }
  set cameraName(value: string) {
    this._cameraName = value;
  }

  /**
   * Serialize message to binary data
   * @param instance message instance
   */
  serializeBinary() {
    const writer = new BinaryWriter();
    GetPhotoRequest.serializeBinaryToWriter(this, writer);
    return writer.getResultBuffer();
  }

  /**
   * Cast message to standard JavaScript object (all non-primitive values are deeply cloned)
   */
  toObject(): GetPhotoRequest.AsObject {
    return {
      cameraName: this.cameraName
    };
  }

  /**
   * Convenience method to support JSON.stringify(message), replicates the structure of toObject()
   */
  toJSON() {
    return this.toObject();
  }

  /**
   * Cast message to JSON using protobuf JSON notation: https://developers.google.com/protocol-buffers/docs/proto3#json
   * Attention: output differs from toObject() e.g. enums are represented as names and not as numbers, Timestamp is an ISO Date string format etc.
   * If the message itself or some of descendant messages is google.protobuf.Any, you MUST provide a message pool as options. If not, the messagePool is not required
   */
  toProtobufJSON(
    // @ts-ignore
    options?: ToProtobufJSONOptions
  ): GetPhotoRequest.AsProtobufJSON {
    return {
      cameraName: this.cameraName
    };
  }
}
export module GetPhotoRequest {
  /**
   * Standard JavaScript object representation for GetPhotoRequest
   */
  export interface AsObject {
    cameraName: string;
  }

  /**
   * Protobuf JSON representation for GetPhotoRequest
   */
  export interface AsProtobufJSON {
    cameraName: string;
  }
}

/**
 * Message implementation for greet.GetPhotoReply
 */
export class GetPhotoReply implements GrpcMessage {
  static id = 'greet.GetPhotoReply';

  /**
   * Deserialize binary data to message
   * @param instance message instance
   */
  static deserializeBinary(bytes: ByteSource) {
    const instance = new GetPhotoReply();
    GetPhotoReply.deserializeBinaryFromReader(
      instance,
      new BinaryReader(bytes)
    );
    return instance;
  }

  /**
   * Check all the properties and set default protobuf values if necessary
   * @param _instance message instance
   */
  static refineValues(_instance: GetPhotoReply) {
    _instance.data = _instance.data || '';
    _instance.success = _instance.success || false;
  }

  /**
   * Deserializes / reads binary message into message instance using provided binary reader
   * @param _instance message instance
   * @param _reader binary reader instance
   */
  static deserializeBinaryFromReader(
    _instance: GetPhotoReply,
    _reader: BinaryReader
  ) {
    while (_reader.nextField()) {
      if (_reader.isEndGroup()) break;

      switch (_reader.getFieldNumber()) {
        case 1:
          _instance.data = _reader.readString();
          break;
        case 2:
          _instance.success = _reader.readBool();
          break;
        default:
          _reader.skipField();
      }
    }

    GetPhotoReply.refineValues(_instance);
  }

  /**
   * Serializes a message to binary format using provided binary reader
   * @param _instance message instance
   * @param _writer binary writer instance
   */
  static serializeBinaryToWriter(
    _instance: GetPhotoReply,
    _writer: BinaryWriter
  ) {
    if (_instance.data) {
      _writer.writeString(1, _instance.data);
    }
    if (_instance.success) {
      _writer.writeBool(2, _instance.success);
    }
  }

  private _data: string;
  private _success: boolean;

  /**
   * Message constructor. Initializes the properties and applies default Protobuf values if necessary
   * @param _value initial values object or instance of GetPhotoReply to deeply clone from
   */
  constructor(_value?: RecursivePartial<GetPhotoReply.AsObject>) {
    _value = _value || {};
    this.data = _value.data;
    this.success = _value.success;
    GetPhotoReply.refineValues(this);
  }
  get data(): string {
    return this._data;
  }
  set data(value: string) {
    this._data = value;
  }
  get success(): boolean {
    return this._success;
  }
  set success(value: boolean) {
    this._success = value;
  }

  /**
   * Serialize message to binary data
   * @param instance message instance
   */
  serializeBinary() {
    const writer = new BinaryWriter();
    GetPhotoReply.serializeBinaryToWriter(this, writer);
    return writer.getResultBuffer();
  }

  /**
   * Cast message to standard JavaScript object (all non-primitive values are deeply cloned)
   */
  toObject(): GetPhotoReply.AsObject {
    return {
      data: this.data,
      success: this.success
    };
  }

  /**
   * Convenience method to support JSON.stringify(message), replicates the structure of toObject()
   */
  toJSON() {
    return this.toObject();
  }

  /**
   * Cast message to JSON using protobuf JSON notation: https://developers.google.com/protocol-buffers/docs/proto3#json
   * Attention: output differs from toObject() e.g. enums are represented as names and not as numbers, Timestamp is an ISO Date string format etc.
   * If the message itself or some of descendant messages is google.protobuf.Any, you MUST provide a message pool as options. If not, the messagePool is not required
   */
  toProtobufJSON(
    // @ts-ignore
    options?: ToProtobufJSONOptions
  ): GetPhotoReply.AsProtobufJSON {
    return {
      data: this.data,
      success: this.success
    };
  }
}
export module GetPhotoReply {
  /**
   * Standard JavaScript object representation for GetPhotoReply
   */
  export interface AsObject {
    data: string;
    success: boolean;
  }

  /**
   * Protobuf JSON representation for GetPhotoReply
   */
  export interface AsProtobufJSON {
    data: string;
    success: boolean;
  }
}
