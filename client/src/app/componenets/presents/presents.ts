import { Component, OnInit, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DataViewModule } from 'primeng/dataview';
import { SelectButtonModule } from 'primeng/selectbutton';
import { TagModule } from 'primeng/tag';
import { ButtonModule } from 'primeng/button';
import { PresentService } from '../../services/presentService/present-service';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { environment } from '../../../envirement/envirement';
import { Present } from '../../models/Present';
import { LotteryResult, Purchase } from '../../models/Purchase';
import { CartService } from '../../services/cartService/cart-service';
import { AuthService } from '../../services/authService/auth-service';
import { PurchasesService } from '../../services/purchasService/purchases-service';
import { LotteryService } from '../../services/lotteryService/lottery-service';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { provideAnimations } from '@angular/platform-browser/animations';

@Component({
  selector: 'app-presents',
  imports: [CurrencyPipe, DataViewModule, SelectButtonModule, TagModule, ButtonModule, FormsModule, CommonModule, ToastModule],
  templateUrl: './presents.html',
  styleUrl: './presents.css',
  providers: [
    MessageService,
    provideAnimations()
  ]
})
export class Presents implements OnInit {
  environment = environment
  private presentSrv = inject(PresentService);
  private cartSrv: CartService = inject(CartService);
  private messageService = inject(MessageService);

  authSrv = inject(AuthService)
  purchaseSrv = inject(PurchasesService)
  lotterySrv = inject(LotteryService)
  winners = signal<any[]>([]);
  products = signal<any>([]);
  products2 = signal<any>([]);

  options: any[] = ['list', 'grid'];
  layout: 'list' | 'grid' = 'grid'; ngOnInit() {
    this.presentSrv.getAllPresents().subscribe((data) => {
      this.products.set([...data.slice(0, 12) || []]);
    });
    const id = this.authSrv.getUserId()

    this.cartSrv.getDraftPurchases(id!).subscribe((data) => {
      this.products2.set(data)
    })
    this.lotterySrv.getAllWinners().subscribe((data) => {
      this.winners.set(data)
    })
  }

  addToCart(present: Present) {

    const id = this.authSrv.getUserId()

    console.log("products", this.products2());
    console.log(present, "present");

    const existingPurchase = this.products2().find((p: Purchase) => p.presentId == present.id);
    console.log("exist", existingPurchase);

    if (existingPurchase) {
      existingPurchase.numOfTickets! += 1;
      this.purchaseSrv.update(existingPurchase.id!, existingPurchase).subscribe({
        next: (data) => {
          console.log(data)
          this.messageService.add({
            severity: 'success',
            summary: 'הצלחה!',
            detail: `${present.name} נוסף לסל בהצלחה`,
            life: 3000
          });
          this.refreshCart(id!);
        }
      });

    }
    else {
      let purchase: Purchase = {
        presentId: present.id,
        present: present,
        numOfTickets: 1,
        userId: id!,
        isDraft: true
      }

      this.cartSrv.addPresentToCart(purchase).subscribe({
        next: (data) => {
          console.log(data)
          this.messageService.add({
            severity: 'success',
            summary: 'הצלחה!',
            detail: `${present.name} נוסף לסל בהצלחה`,
            life: 3000
          }); this.refreshCart(id!);

        },
        error: (err) => console.log(err)
      });
    }



  }
  refreshCart(userId: number) {
    this.cartSrv.getDraftPurchases(userId).subscribe((data) => {
      this.products2.set(data);
    });
  }

  getWinnerName(presentId: number): string {
    console.log(presentId);

    const list = this.winners()
    const winner = list.find((w: any) => {
      console.log(`Comparing DB_ID: ${w.presentId || w.PresentId} with ID: ${presentId}`);
      return (w.presentId == presentId || w.PresentId == presentId);
    }); return winner ? winner.winner.firstName + ' ' + winner.winner.lastName : 'טרם נקבע';
  }
}

