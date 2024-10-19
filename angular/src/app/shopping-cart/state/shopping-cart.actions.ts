import { createAction, props } from "@ngrx/store";
import { Item } from "@components/models";

export const ItemAddedToShoppingCart = createAction(
	"[Shopping Cart] - Item agregado al carrito de compras",
	props<{ item: Item }>()
);

export const CloseButtonClicked = createAction(
	"[Shopping Cart] - Shopping cart closed"
);

export const BuyButtonClicked = createAction(
	"[Shopping Cart] - Shopping Cart buy button clicked"
);
export const BuyButtonWithItemsAndTotal = createAction(
	"[Shopping Cart] - Shopping Cart buy button clicked",
	props<{ items: Item[]; montoTotal: number }>()
);
export const CheckOutItems = createAction(
	"[Shopping Cart] - Checkout items",
	props<{ items: Item[]; montoTotal: number }>()
);
export const EmptyButtonClicked = createAction(
	"[Shopping Cart] - Shopping cart empty button clicked"
);
