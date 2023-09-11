export class User{
    constructor(
        id?: number,
        username: string = '',
        email: string = '',
        password:string = '',
        createdDate: string = '',
        lastModifiedDate: string = '',
        )
    {
        this.id=id || 0,
        this.username=username,
        this.email = email,
        this.password = password,
        this.createdDate=createdDate,
        this.lastModifiedDate=lastModifiedDate
    }

    id: number;
    username="";
    email="";
    password="";
    createdDate="";
    lastModifiedDate="";

}