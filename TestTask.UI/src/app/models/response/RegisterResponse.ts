import { User } from "../User";
import { BaseResponse } from "./BaseResponse";

export class RegisterResponse extends BaseResponse{
    data:User = new User();

}