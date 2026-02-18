import { Donor } from '../../../models/Purchase';
import { DonorService } from '../../../services/donorService/donor-service';
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
import { OrderListModule } from 'primeng/orderlist';
import { provideAnimations } from '@angular/platform-browser/animations';
import { Presents } from '../../presents/presents';
import { Present } from '../../../models/Present';
import { environment } from '../../../../envirement/envirement';
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
  selector: 'app-donors-list',
  imports: [RatingModule,CommonModule,ButtonModule,
     CommonModule,ConfirmDialogModule, DialogModule, SelectModule, 
    FileUploadModule, IconFieldModule, InputIconModule, OrderListModule
    ,InputNumberModule, RadioButtonModule, RatingModule, TableModule,
     TagModule, ToastModule, ToolbarModule, InputTextModule, FormsModule],
  templateUrl: './donors-list.html',
  styleUrl: './donors-list.css',
  providers: [ provideAnimations(), MessageService, ConfirmationService]

})
export class DonorsList {
 private donorSrv = inject(DonorService);
    private messageService = inject(MessageService);
    private confirmationService = inject(ConfirmationService);
    donorDialog: boolean = false;
    donors!: Donor[];
    donor!: Donor;
    selectedDonors!: Donor[] | null;
    submitted: boolean = false;
    statuses!: any[];
    cols!: Column[];
    exportColumns!: ExportColumn[];
    products = signal<Present[]>([]);
    visible:boolean = false
      environment = environment
    ngOnInit() {
        this.donorSrv.getAllDonors().subscribe({
            next: (data) => this.donors = data,
            error:(err)=>console.log(err)
        });
        this.statuses = [
            { label: 'INSTOCK', value: 'instock' },
            { label: 'LOWSTOCK', value: 'lowstock' },
            { label: 'OUTOFSTOCK', value: 'outofstock' }
        ];
        this.cols = [
            { field: 'id', header: 'Code', customExportHeader: 'Product Code' },
            { field: 'name', header: 'Name' },
            { field: 'email', header: 'Email' },
            { field: 'numOfPresents', header: 'Number of Presents' },
            { field: 'presents', header: 'Presents' }
        ];
        this.exportColumns = this.cols.map((col) => ({ title: col.header, dataKey: col.field }));
        this.donors = []
    }
  showDialog(donor:Donor) {
    this.products.set( donor.presents) 
        this.visible = true;
    }
    loadDonors() {
  this.donorSrv.getAllDonors().subscribe(data => {
    this.donors = data;
  });
}
    openNew() {
        this.donor = {} as Donor;
        this.submitted = false;
        this.donorDialog = true;
    }

    editDonor(donor: Donor) {
        // this.donorSrv.update(donor.id, donor).subscribe({
        //     error:(err)=>console.log(err)
        // });
        
        this.donor = { ...donor };
        this.donorDialog = true;
    }

    deleteSelectedDonors() {
        this.confirmationService.confirm({
            message: 'Are you sure you want to delete the selected donors?',
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
                
                    this.selectedDonors?.map((d)=>{
                        console.log("2");
                               this.donorSrv.deleteDonor(d.id!).subscribe({
                    error:(err)=>{console.log(err)

                        if(err.status==400)
                        {
                            this.messageService.add({
                    severity: 'error',
                    summary: 'Error',
                    detail: 'לא ניתן למחוק את התורם כיוון שקיימת מתנה על שמו',
                    life: 3000
                     })
                        }
                    },
                    next:() =>{
                        this.messageService.add({
                    severity: 'success',
                    summary: 'Successful',
                    detail: 'Donors Deleted',
                    life: 3000

                     })
                   this.selectedDonors = null;

                                       this.loadDonors()
                    },
                                  

                });
                        console.log("3");
                        
                    })
               
            }
        });
    }

    hideDialog() {
        this.donorDialog = false;
        this.submitted = false;
    }

    deleteDonor(donor: Donor) {
        console.log(donor);
        
        this.confirmationService.confirm({
            message: 'Are you sure you want to delete ' + donor.name + '?',
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
                this.donorSrv.deleteDonor(donor.id!).subscribe({
                    
                    error:(err)=>{
                        console.log(err)
                         if(err.status==400)
                        {
                            this.messageService.add({
                    severity: 'error',
                    summary: 'Error',
                    detail: 'לא ניתן למחוק את התורם כיוון שקיימת מתנה על שמו',
                    life: 3000

                     })
                        }
                    },
                    next:()=>{

                    
                     this.messageService.add({
                    severity: 'success',
                    summary: 'Successful',
                    detail: 'Donor Deleted',
                    life: 3000

                })
                       this.donor  = {} as Donor
                                       this.loadDonors()

                }
            })
        }
    })
    }
    findIndexById(id: number): number {
        let index = -1;
        for (let i = 0; i < this.donors.length; i++) {
            if (this.donors[i].id == id) {
                index = i;
                break;
            }
        }
        
        return index;
    }



    saveDonor() {
        this.submitted = true;
        if (this.donor.name?.trim()) {
            console.log(this.donor);
            
            if (this.donor.id) {
                this.donorSrv.updateDonor(this.donor).subscribe(()=>{
                    this.messageService.add({
                    severity: 'success',
                    summary: 'Successful',
                    detail: 'Product Updated',
                    life: 3000
                });
                    this.loadDonors()
                })
                
            } else {
                this.donorSrv.createDonor(this.donor).subscribe(()=>{
                      this.messageService.add({
                    severity: 'success',
                    summary: 'Successful',
                    detail: 'Product Created',
                    life: 3000
                });
                this.loadDonors()
                })
              
            }

            this.donors = [...this.donors];
            this.donorDialog = false;
            this.donor = {} as Donor;
        }
    }
}


