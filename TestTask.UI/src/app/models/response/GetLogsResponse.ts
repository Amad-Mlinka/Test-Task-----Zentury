import { Log } from "../Log";
import { BaseResponse } from "./BaseResponse";

export class GetLogsResponse extends BaseResponse{
    data:Log[]=[];
}