import { RefreshToken } from "../RefreshToken";
import { User } from "../User";
import { BaseResponse } from "./BaseResponse";

export class LoginResponse extends BaseResponse {
    data: User=new User();
    token: string="";
    refreshToken: RefreshToken = new RefreshToken();
}