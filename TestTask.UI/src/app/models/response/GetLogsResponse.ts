import { Log } from "../Log";
import { BaseResponse } from "./BaseResponse";

export class GetLogsResponse extends BaseResponse{
    data : Log[] = [];
    count : number = 0 ;
    pageNumber : number = 0;
    pageSize : number = 0;
    totalCount : number = 0;
}