import { Component, OnInit, inject, signal } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { DataViewModule } from 'primeng/dataview';
import { TagModule } from 'primeng/tag';
import { CartService } from '../../services/cartService/cart-service';
import { CommonModule, NgClass ,NgFor} from '@angular/common';
import { Present } from '../../models/Present';
import { Purchase } from '../../models/Purchase';
import { endWith } from 'rxjs';
import { environment } from '../../../envirement/envirement';
import { AuthService } from '../../services/authService/auth-service';
import { PurchasesService } from '../../services/purchasService/purchases-service';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-cart',
  imports: [ButtonModule,DataViewModule,TagModule,NgClass,NgFor,CommonModule,ToastModule],
  templateUrl: './cart.html',
  styleUrl: './cart.css',
  providers:[MessageService]
})
export class Cart implements OnInit{
   private cartSrv = inject(CartService);
      private purchaseSrv = inject(PurchasesService);
  private messageService:MessageService = inject(MessageService)

    products = signal<Purchase[]>([]);
    authSrv = inject(AuthService)
environment = environment
    ngOnInit() {
this.loadCart()
    }
    loadCart(){
      const id = this.authSrv.getUserId()
        this.cartSrv.getDraftPurchases(id!).subscribe((data) => {
          console.log('Data from server:', data); 
            this.products.set(data);
        });
    }
deleteItem(presentId:number)
{
  this.cartSrv.deletePresentFromCart(presentId).subscribe(() => {
    const updatedProducts = this.products().filter(item => item.id !== presentId);
    this.products.set(updatedProducts);
  });
}

checkoutAll(){
  const userId = this.authSrv.getUserId()


const cartItems = this.products();

  cartItems.forEach(item => {
     item.isDraft=false

    this.purchaseSrv.update(item.id!, item).subscribe({
      next: () => {
        console.log(`Purchase ${item.id} updated successfully`);
        
      },
      error: (err) => {
        console.error(`Failed to update purchase ${item.id}`, err);
      }
    });
  });
this.loadCart()
  this.messageService.add({ 
    severity: 'success', 
    summary: 'הרכישה הושלמה', 
    detail: 'כל המוצרים בסל נרכשו בהצלחה!' 
  });
  
  this.loadCart();}

calculateTotal()
{
return this.products().reduce((acc, item) => acc + (item.present!.price * item.numOfTickets!), 0);
}
}
