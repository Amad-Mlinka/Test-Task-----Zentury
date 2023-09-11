import { User } from '../User';
import { BaseResponse } from './BaseResponse';

export class GetUsersResponse extends BaseResponse{
  data: User[] = [];
}
