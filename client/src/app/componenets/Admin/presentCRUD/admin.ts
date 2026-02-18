import { bootstrapApplication } from '@angular/platform-browser';
import { Component, OnInit, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { Dialog, DialogModule } from 'primeng/dialog';
import { SelectModule } from 'primeng/select';
import { FileUploadModule } from 'primeng/fileupload';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputNumberModule } from 'primeng/inputnumber';
import { RadioButtonModule } from 'primeng/radiobutton';
import { RatingModule } from 'primeng/rating';
import { Table, TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { InputTextModule } from 'primeng/inputtext';
import { MessageService, ConfirmationService } from 'primeng/api';
import { CommonModule } from '@angular/common';
import { provideAnimations } from '@angular/platform-browser/animations';
import { PresentService } from '../../../services/presentService/present-service';
import { Present } from '../../../models/Present';
import { Category, Donor, LotteryResult, Purchase } from '../../../models/Purchase';
import { CategoryService } from '../../../services/categoryService/category-service';
import { DonorService } from '../../../services/donorService/donor-service';
import { environment } from '../../../../envirement/envirement';

import { OrderListModule } from 'primeng/orderlist';
import { PurchasesService } from '../../../services/purchasService/purchases-service';
import { LotteryService } from '../../../services/lotteryService/lottery-service';
import { LoteryResultService } from '../../../services/LoteryResultService/lotery-result-service';


interface Column {
    field: string;
    header: string;
    customExportHeader?: string;
}

interface ExportColumn {
    title: string;
    dataKey: string;
}

@Component({
    standalone: true,
  selector: 'app-admin',
  imports: [RatingModule,CommonModule,ButtonModule,OrderListModule, ConfirmDialogModule,
     DialogModule, SelectModule, FileUploadModule, IconFieldModule,
      InputIconModule, InputNumberModule, 
    RadioButtonModule, RatingModule, TableModule,
     TagModule, ToastModule, ToolbarModule, 
     InputTextModule, FormsModule],
  templateUrl: './admin.html',
  styleUrls: ['./admin.css'],
  providers: [ provideAnimations(), MessageService, ConfirmationService,CommonModule]
})

export class Admin implements OnInit {
   private presentService = inject(PresentService);
    private messageService = inject(MessageService);
    private confirmationService = inject(ConfirmationService);
    private lotterySrv = inject(LoteryResultService)
    private lotterySrv2 = inject(LotteryService)

    presentDialog: boolean = false;
    presents!: Present[];
    present!: Present;
    donors:Donor[]=[]
    selectedPresents!: Present[] | null;
    submitted: boolean = false;
    categories!: Category[];
    cols!: Column[];
    winners = signal<any[]>([]);
      environment = environment
        products = signal<Purchase[]>([]);
    visible:boolean = false
    visible2:boolean = false

    winner:LotteryResult|null=null
    exportColumns!: ExportColumn[];
catsServ = inject(CategoryService)
donorSrv = inject(DonorService)
purchaseSrv:PurchasesService = inject(PurchasesService)
    ngOnInit() {

        this.lotterySrv2.getAllWinners().subscribe((data)=>{
this.winners.set(data) 
})

        this.presentService.getAllPresents().subscribe({
            next: (data) => this.presents = data as Present[],
            error:(err)=>console.log(err)

        });
       this.loadDonors()
        this.catsServ.getAllCategories().subscribe((data)=>{
this.categories = data as Category[]
       })
        this.cols = [
            { field: 'id', header: 'Code', customExportHeader: 'Product Code' },
            { field: 'name', header: 'Name' },
            { field: 'imageUrl', header: 'Image' },
            { field: 'price', header: 'Price' },
            { field: 'category', header: 'Category' },
            { field: 'donor', header: 'Donor' },
        ];
        this.exportColumns = this.cols.map((col) => ({ title: col.header, dataKey: col.field }));
        this.presents=[]
    }
    loadPresents() {
  this.presentService.getAllPresents().subscribe(data => {
    this.presents = data;
  });
}
  showDialog(present:Present) {
this.purchaseSrv.getPurchasesForPresent(present.id).subscribe((data)=>{
 this.products.set(data) 
        this.visible = true;
})
   
    }
    openNew() {
        this.present = {} as Present;
        this.submitted = false;
        this.presentDialog = true;
                console.log(this.presents);
                console.log(this.present);
    }

    editPresent(present: Present) {
        // this.presentService.update(present.id,present).subscribe({
        //     error:(err)=>console.log(err)
        // });
                console.log(this.presents);

        this.present = { ...present };
        this.presentDialog = true;
    }

    loadDonors() {
  this.donorSrv.getAllDonors().subscribe(data => {
    this.donors = data;
  });
}
runLottery(id:number)
{

   this.confirmationService.confirm({
            message: 'Are you ready to make raffle?',
            header: 'Confirm',
            icon: 'pi pi-exclamation-triangle',
            rejectButtonProps: {
                label: 'No',
                severity: 'secondary',
                variant: 'text'
            },
            acceptButtonProps: {
                severity: 'danger',
                label: 'Yes'
            },
            accept: () => {
                this.lotterySrv.getById(id).subscribe((data)=>{
            if(!data)
             {
        this.messageService.add({
      severity: 'error',
      summary: 'Error',
      detail: 'לא נמצאו רכישות למתנה זו',
      life: 3000
    });}

    else{
        this.winner=data
        this.visible2 = true;

    }
               }) 
             }             

            })
        }
    

    //////
    

    deleteSelectedPresents() {
        this.confirmationService.confirm({
            message: 'Are you sure you want to delete the selected presents?',
            header: 'Confirm',
            icon: 'pi pi-exclamation-triangle',
            rejectButtonProps: {
                label: 'No',
                severity: 'secondary',
                variant: 'text'
            },
            acceptButtonProps: {
                severity: 'danger',
                label: 'Yes'
            },
            accept: () => {
                console.log("1");
                
  this.selectedPresents?.map((d)=>{
    
                        this.deletePresent(d)
                        
                    })   
                this.selectedPresents = null;
                this.messageService.add({
                    severity: 'success',
                    summary: 'Successful',
                    detail: 'Presents Deleted',
                    life: 3000
                });
            }
        });
    }

    hideDialog() {
        
        this.presentDialog = false;
        this.submitted = false;
    }

    deletePresent(present: Present) {
                    console.log(present);

        this.confirmationService.confirm({
            
            message: 'Are you sure you want to delete ' + present.name + '?',
            header: 'Confirm',
            icon: 'pi pi-exclamation-triangle',
            rejectButtonProps: {
                label: 'No',
                severity: 'secondary',
                variant: 'text'
            },
            acceptButtonProps: {
                severity: 'danger',
                label: 'Yes'
            },
            accept: () => {
                this.presentService.delete(present.id).subscribe({
                    next: () => {
 this.messageService.add({
                    severity: 'success',
                    summary: 'Successful',
                    detail: 'Present Deleted',
                    life: 3000
                });
                this.present = {} as Present;
                    this.loadPresents()
                    },
                error: (err) => {
                    this.messageService.add({
      severity: 'error',
      summary: 'Error',
      detail: 'Failed to delete present',
      life: 3000
    });
    console.error(err);
                     }
                })
               
            
            }
        });
    }

    findIndexById(id: number): number {
        let index = -1;
        for (let i = 0; i < this.presents.length; i++) {
            if (this.presents[i].id == id) {
                index = i;
                break;
            }
        }
        
        return index;
    }

    // createId(): string {
    //     let id = '';
    //     var chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    //     for (var i = 0; i < 5; i++) {
    //         id += chars.charAt(Math.floor(Math.random() * chars.length));
    //     }
    //     return id;
    // }

    // getSeverity(status: string) {
    //     switch (status) {
    //         case 'INSTOCK':
    //             return 'success';
    //         case 'LOWSTOCK':
    //             return 'warn';
    //         case 'OUTOFSTOCK':
    //             return 'danger';
    //     }
    // }

    savePresent() {
        this.submitted = true;
        if (this.present.name?.trim()) {
            if (this.present.id) {
              /////////
                              console.log(this.present);

               this.presentService.update(this.present.id, this.present).subscribe({
                    next: () => {
 this.messageService.add({
                    severity: 'success',
                    summary: 'Successful',
                    detail: 'Present Updated',
                    life: 3000
                });
                this.present = {} as Present;
                    this.loadPresents()
                    },
                error: (err) => {
                    this.messageService.add({
      severity: 'error',
      summary: 'Error',
      detail: 'Failed to update present',
      life: 3000
    });
    console.error(err);
                     }
                })

                /////////

            } else {
                console.log(this.present);
                
                 this.presentService.create(this.present).subscribe({
                    next: () => {
 this.messageService.add({
                    severity: 'success',
                    summary: 'Successful',
                    detail: 'Present Created',
                    life: 3000
                });
                this.present = {} as Present;
                    this.loadPresents()
                    },
                error: (err) => {
                    this.messageService.add({
      severity: 'error',
      summary: 'Error',
      detail: 'Failed to create present',
      life: 3000
    });
    console.error(err);
                     }
                })
            }

            this.presents = [...this.presents];
            this.presentDialog = false;
            this.present = {} as Present;
        }
    }


    //exports...


    exportRevenueReport() {
    const data = this.presents.map(p => ({
      'שם המתנה': p.name,
      'מחיר כרטיס': p.price,
      'כרטיסים שנמכרו': p.numOfPurchases,
      'סך הכנסה': (p.price * p.numOfPurchases!) 
    }));
    
    this.downloadCSV(data, 'Revenue_Report');
  }

  exportWinnersReport() {
    const data = this.presents
      .filter(p => p.isLotteryDone) 
      .map(p => ({
        'ID מתנה': p.id,
        'שם המתנה': p.name,
        'שם הזוכה': this.getWinnerName(p.id) 
      }));

    this.downloadCSV(data, 'Winners_Report');
  }

  private downloadCSV(data: any[], fileName: string) {
    if (data.length === 0) return;

    const replacer = (key: any, value: any) => value === null ? '' : value;
    const header = Object.keys(data[0]);
    const csv = data.map(row => 
      header.map(fieldName => JSON.stringify(row[fieldName], replacer)).join(',')
    );
    csv.unshift(header.join(','));
    const csvArray = csv.join('\r\n');

const blob = new Blob(['\ufeff' + csvArray], { type: 'text/csv;charset=utf-8;' });    const link = document.createElement('a');
    const url = URL.createObjectURL(blob);
    link.setAttribute('href', url);
    link.setAttribute('download', `${fileName}.csv`);
    link.style.visibility = 'hidden';
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }
  getWinnerName(presentId: number): string {
  console.log(presentId);
  
  const list = this.winners()
const winner = list.find((w: any) => {
    console.log(`Comparing DB_ID: ${w.presentId || w.PresentId} with ID: ${presentId}`);
    return (w.presentId == presentId || w.PresentId == presentId);
  });  return winner ? winner.winner.firstName + ' '+ winner.winner.lastName : 'טרם נקבע'; 

}

}