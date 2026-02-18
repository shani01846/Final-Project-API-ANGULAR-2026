import { Donor, Purchase } from "./Purchase";

export class Present
{
    id!:number;
    name!:string;
    description:string="";
    imageUrl:string="";
    categoryName?:string;
    categoryId?:number;
    donorName!:string;
    donorId?:number
    price!:number;
    isLotteryDone:boolean=false;
    purchases:Purchase[]=[];
    numOfPurchases?:number
} 