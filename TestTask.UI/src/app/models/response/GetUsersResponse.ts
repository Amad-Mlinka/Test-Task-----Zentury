import { User } from "../User";
import { BaseResponse } from "./BaseResponse";

export class GetUsersResponse extends BaseResponse{
    data : User[] = [];
    count : number = 0 ;
    pageNumber : number = 0;
    pageSize : number = 0;
    totalCount : number = 0;
}