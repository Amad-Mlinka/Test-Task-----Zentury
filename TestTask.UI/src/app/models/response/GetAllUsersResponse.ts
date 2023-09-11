import { User } from '../User';
import { BaseResponse } from './BaseResponse';

export class GetAllUsersResponse extends BaseResponse{
  data: User[] = [];
}
