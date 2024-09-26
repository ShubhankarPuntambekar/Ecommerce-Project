import { Component, inject, input } from '@angular/core';
import { CartItem } from '../../../shared/modules/cart';
import { RouterLink } from '@angular/router';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { CurrencyPipe } from '@angular/common';
import { CartService } from '../../../core/services/cart.service';

@Component({
  selector: 'app-cart-item',
  standalone: true,
  imports: [RouterLink, MatButton, MatIcon, CurrencyPipe],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.scss',
})
export class CartItemComponent {
  item = input.required<CartItem>();
  cartServive = inject(CartService);

  incrementQuant() {
    this.cartServive.addItemToCart(this.item());
  }

  decrementQuant() {
    this.cartServive.removeItemFromCart(this.item().productId);
  }

  removeItemFromCart() {
    this.cartServive.removeItemFromCart(
      this.item().productId,
      this.item().quantity
    );
  }
}
