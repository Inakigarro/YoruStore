import { createAction, props } from "@ngrx/store";
import { Item } from "@components/models";

export const ItemAddedToShoppingCart = createAction(
	"[Shopping Cart] - Item agregado al carrito de compras",
	props<{ item: Item }>()
);
