import { Present } from "./Present";

 export class Purchase
{
      id?:number;
      userId?:number;
      presentId!:number;
      Created_At?:string;
      present?:Present;
      userName?:string
      user?:User;
      numOfTickets!:number;
      isDraft:boolean=true;
}
    export class User
{
      id?:number;
      firstName!:string;
      lastName!:string;
      email!:string;
      address!:string;
      password!:string;
      phone!:string;
      created_At?:Date;
}

export class Category
{
    id?:number;
    name!:string;
}
export class Donor
{
    id?:number;
    name!:string;
    email!:string;
    created_At!:Date;
    presents:Present[]=[];
}
export class LotteryResult
{
    id?:number;
    presentId?:number;
    present?:Present;
    userId?:number;
    user?:User;
    createdAt?:Date;
}