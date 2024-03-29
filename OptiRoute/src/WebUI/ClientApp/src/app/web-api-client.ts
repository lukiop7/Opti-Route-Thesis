/* tslint:disable */
/* eslint-disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.9.4.0 (NJsonSchema v10.3.1.0 (Newtonsoft.Json v12.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming

import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

export interface IBenchmarksClient {
    getSolution(file: FileParameter | null | undefined): Observable<SolutionDto>;
    getBenchmarkResults(): Observable<BenchmarkResultDto[]>;
    getSolutionByBenchmarkResultId(benchmarkResultId: number): Observable<SolutionDto>;
    getBestSolutionByBenchmarkResultId(benchmarkResultId: number): Observable<SolutionDto>;
}

@Injectable({
    providedIn: 'root'
})
export class BenchmarksClient implements IBenchmarksClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    getSolution(file: FileParameter | null | undefined): Observable<SolutionDto> {
        let url_ = this.baseUrl + "/api/Benchmarks";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = new FormData();
        if (file !== null && file !== undefined)
            content_.append("file", file.data, file.fileName ? file.fileName : "file");

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetSolution(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetSolution(<any>response_);
                } catch (e) {
                    return <Observable<SolutionDto>><any>_observableThrow(e);
                }
            } else
                return <Observable<SolutionDto>><any>_observableThrow(response_);
        }));
    }

    protected processGetSolution(response: HttpResponseBase): Observable<SolutionDto> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = SolutionDto.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<SolutionDto>(<any>null);
    }

    getBenchmarkResults(): Observable<BenchmarkResultDto[]> {
        let url_ = this.baseUrl + "/api/Benchmarks";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetBenchmarkResults(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetBenchmarkResults(<any>response_);
                } catch (e) {
                    return <Observable<BenchmarkResultDto[]>><any>_observableThrow(e);
                }
            } else
                return <Observable<BenchmarkResultDto[]>><any>_observableThrow(response_);
        }));
    }

    protected processGetBenchmarkResults(response: HttpResponseBase): Observable<BenchmarkResultDto[]> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(BenchmarkResultDto.fromJS(item));
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<BenchmarkResultDto[]>(<any>null);
    }

    getSolutionByBenchmarkResultId(benchmarkResultId: number): Observable<SolutionDto> {
        let url_ = this.baseUrl + "/api/Benchmarks/{benchmarkResultId}/Solution";
        if (benchmarkResultId === undefined || benchmarkResultId === null)
            throw new Error("The parameter 'benchmarkResultId' must be defined.");
        url_ = url_.replace("{benchmarkResultId}", encodeURIComponent("" + benchmarkResultId));
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetSolutionByBenchmarkResultId(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetSolutionByBenchmarkResultId(<any>response_);
                } catch (e) {
                    return <Observable<SolutionDto>><any>_observableThrow(e);
                }
            } else
                return <Observable<SolutionDto>><any>_observableThrow(response_);
        }));
    }

    protected processGetSolutionByBenchmarkResultId(response: HttpResponseBase): Observable<SolutionDto> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = SolutionDto.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<SolutionDto>(<any>null);
    }

    getBestSolutionByBenchmarkResultId(benchmarkResultId: number): Observable<SolutionDto> {
        let url_ = this.baseUrl + "/api/Benchmarks/{benchmarkResultId}/BestSolution";
        if (benchmarkResultId === undefined || benchmarkResultId === null)
            throw new Error("The parameter 'benchmarkResultId' must be defined.");
        url_ = url_.replace("{benchmarkResultId}", encodeURIComponent("" + benchmarkResultId));
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetBestSolutionByBenchmarkResultId(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetBestSolutionByBenchmarkResultId(<any>response_);
                } catch (e) {
                    return <Observable<SolutionDto>><any>_observableThrow(e);
                }
            } else
                return <Observable<SolutionDto>><any>_observableThrow(response_);
        }));
    }

    protected processGetBestSolutionByBenchmarkResultId(response: HttpResponseBase): Observable<SolutionDto> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = SolutionDto.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<SolutionDto>(<any>null);
    }
}

export interface ICVRPTWClient {
    getSolution(problem: ProblemDto): Observable<SolutionDto>;
}

@Injectable({
    providedIn: 'root'
})
export class CVRPTWClient implements ICVRPTWClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    getSolution(problem: ProblemDto): Observable<SolutionDto> {
        let url_ = this.baseUrl + "/api/CVRPTW";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(problem);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetSolution(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetSolution(<any>response_);
                } catch (e) {
                    return <Observable<SolutionDto>><any>_observableThrow(e);
                }
            } else
                return <Observable<SolutionDto>><any>_observableThrow(response_);
        }));
    }

    protected processGetSolution(response: HttpResponseBase): Observable<SolutionDto> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = SolutionDto.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<SolutionDto>(<any>null);
    }
}

export class SolutionDto implements ISolutionDto {
    feasible?: boolean;
    routes?: RouteDto[] | undefined;
    depot?: DepotDto | undefined;
    distance?: number;
    time?: number;

    constructor(data?: ISolutionDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.feasible = _data["feasible"];
            if (Array.isArray(_data["routes"])) {
                this.routes = [] as any;
                for (let item of _data["routes"])
                    this.routes!.push(RouteDto.fromJS(item));
            }
            this.depot = _data["depot"] ? DepotDto.fromJS(_data["depot"]) : <any>undefined;
            this.distance = _data["distance"];
            this.time = _data["time"];
        }
    }

    static fromJS(data: any): SolutionDto {
        data = typeof data === 'object' ? data : {};
        let result = new SolutionDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["feasible"] = this.feasible;
        if (Array.isArray(this.routes)) {
            data["routes"] = [];
            for (let item of this.routes)
                data["routes"].push(item.toJSON());
        }
        data["depot"] = this.depot ? this.depot.toJSON() : <any>undefined;
        data["distance"] = this.distance;
        data["time"] = this.time;
        return data; 
    }
}

export interface ISolutionDto {
    feasible?: boolean;
    routes?: RouteDto[] | undefined;
    depot?: DepotDto | undefined;
    distance?: number;
    time?: number;
}

export class RouteDto implements IRouteDto {
    customers?: CustomerDto[] | undefined;
    totalTime?: number;
    totalDistance?: number;
    totalLoad?: number;

    constructor(data?: IRouteDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            if (Array.isArray(_data["customers"])) {
                this.customers = [] as any;
                for (let item of _data["customers"])
                    this.customers!.push(CustomerDto.fromJS(item));
            }
            this.totalTime = _data["totalTime"];
            this.totalDistance = _data["totalDistance"];
            this.totalLoad = _data["totalLoad"];
        }
    }

    static fromJS(data: any): RouteDto {
        data = typeof data === 'object' ? data : {};
        let result = new RouteDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        if (Array.isArray(this.customers)) {
            data["customers"] = [];
            for (let item of this.customers)
                data["customers"].push(item.toJSON());
        }
        data["totalTime"] = this.totalTime;
        data["totalDistance"] = this.totalDistance;
        data["totalLoad"] = this.totalLoad;
        return data; 
    }
}

export interface IRouteDto {
    customers?: CustomerDto[] | undefined;
    totalTime?: number;
    totalDistance?: number;
    totalLoad?: number;
}

export class CustomerDto implements ICustomerDto {
    id?: number;
    x?: number;
    y?: number;
    demand?: number;
    readyTime?: number;
    dueDate?: number;
    serviceTime?: number;

    constructor(data?: ICustomerDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.x = _data["x"];
            this.y = _data["y"];
            this.demand = _data["demand"];
            this.readyTime = _data["readyTime"];
            this.dueDate = _data["dueDate"];
            this.serviceTime = _data["serviceTime"];
        }
    }

    static fromJS(data: any): CustomerDto {
        data = typeof data === 'object' ? data : {};
        let result = new CustomerDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["x"] = this.x;
        data["y"] = this.y;
        data["demand"] = this.demand;
        data["readyTime"] = this.readyTime;
        data["dueDate"] = this.dueDate;
        data["serviceTime"] = this.serviceTime;
        return data; 
    }
}

export interface ICustomerDto {
    id?: number;
    x?: number;
    y?: number;
    demand?: number;
    readyTime?: number;
    dueDate?: number;
    serviceTime?: number;
}

export class DepotDto implements IDepotDto {
    id?: number;
    x?: number;
    y?: number;
    dueDate?: number;

    constructor(data?: IDepotDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.x = _data["x"];
            this.y = _data["y"];
            this.dueDate = _data["dueDate"];
        }
    }

    static fromJS(data: any): DepotDto {
        data = typeof data === 'object' ? data : {};
        let result = new DepotDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["x"] = this.x;
        data["y"] = this.y;
        data["dueDate"] = this.dueDate;
        return data; 
    }
}

export interface IDepotDto {
    id?: number;
    x?: number;
    y?: number;
    dueDate?: number;
}

export class BenchmarkResultDto implements IBenchmarkResultDto {
    dbId?: number;
    name?: string | undefined;
    bestDistance?: number;
    bestVehicles?: number;
    distance?: number;
    vehicles?: number;
    solutionDbId?: number;
    bestSolutionDbId?: number | undefined;
    benchmarkInstanceDbId?: number;

    constructor(data?: IBenchmarkResultDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.dbId = _data["dbId"];
            this.name = _data["name"];
            this.bestDistance = _data["bestDistance"];
            this.bestVehicles = _data["bestVehicles"];
            this.distance = _data["distance"];
            this.vehicles = _data["vehicles"];
            this.solutionDbId = _data["solutionDbId"];
            this.bestSolutionDbId = _data["bestSolutionDbId"];
            this.benchmarkInstanceDbId = _data["benchmarkInstanceDbId"];
        }
    }

    static fromJS(data: any): BenchmarkResultDto {
        data = typeof data === 'object' ? data : {};
        let result = new BenchmarkResultDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["dbId"] = this.dbId;
        data["name"] = this.name;
        data["bestDistance"] = this.bestDistance;
        data["bestVehicles"] = this.bestVehicles;
        data["distance"] = this.distance;
        data["vehicles"] = this.vehicles;
        data["solutionDbId"] = this.solutionDbId;
        data["bestSolutionDbId"] = this.bestSolutionDbId;
        data["benchmarkInstanceDbId"] = this.benchmarkInstanceDbId;
        return data; 
    }
}

export interface IBenchmarkResultDto {
    dbId?: number;
    name?: string | undefined;
    bestDistance?: number;
    bestVehicles?: number;
    distance?: number;
    vehicles?: number;
    solutionDbId?: number;
    bestSolutionDbId?: number | undefined;
    benchmarkInstanceDbId?: number;
}

export class ProblemDto implements IProblemDto {
    vehicles?: number;
    capacity?: number;
    depot?: DepotDto | undefined;
    customers?: CustomerDto[] | undefined;
    distances?: number[][] | undefined;
    durations?: number[][] | undefined;

    constructor(data?: IProblemDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.vehicles = _data["vehicles"];
            this.capacity = _data["capacity"];
            this.depot = _data["depot"] ? DepotDto.fromJS(_data["depot"]) : <any>undefined;
            if (Array.isArray(_data["customers"])) {
                this.customers = [] as any;
                for (let item of _data["customers"])
                    this.customers!.push(CustomerDto.fromJS(item));
            }
            if (Array.isArray(_data["distances"])) {
                this.distances = [] as any;
                for (let item of _data["distances"])
                    this.distances!.push(item);
            }
            if (Array.isArray(_data["durations"])) {
                this.durations = [] as any;
                for (let item of _data["durations"])
                    this.durations!.push(item);
            }
        }
    }

    static fromJS(data: any): ProblemDto {
        data = typeof data === 'object' ? data : {};
        let result = new ProblemDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["vehicles"] = this.vehicles;
        data["capacity"] = this.capacity;
        data["depot"] = this.depot ? this.depot.toJSON() : <any>undefined;
        if (Array.isArray(this.customers)) {
            data["customers"] = [];
            for (let item of this.customers)
                data["customers"].push(item.toJSON());
        }
        if (Array.isArray(this.distances)) {
            data["distances"] = [];
            for (let item of this.distances)
                data["distances"].push(item);
        }
        if (Array.isArray(this.durations)) {
            data["durations"] = [];
            for (let item of this.durations)
                data["durations"].push(item);
        }
        return data; 
    }
}

export interface IProblemDto {
    vehicles?: number;
    capacity?: number;
    depot?: DepotDto | undefined;
    customers?: CustomerDto[] | undefined;
    distances?: number[][] | undefined;
    durations?: number[][] | undefined;
}

export interface FileParameter {
    data: any;
    fileName: string;
}

export class SwaggerException extends Error {
    message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isSwaggerException = true;

    static isSwaggerException(obj: any): obj is SwaggerException {
        return obj.isSwaggerException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
    if (result !== null && result !== undefined)
        return _observableThrow(result);
    else
        return _observableThrow(new SwaggerException(message, status, response, headers, null));
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader();
            reader.onload = event => {
                observer.next((<any>event.target).result);
                observer.complete();
            };
            reader.readAsText(blob);
        }
    });
}