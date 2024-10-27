import {
	createAction,
	createActionGroup,
	emptyProps,
	props,
} from "@ngrx/store";
import { Item } from "@components/models";

export const ShoppingCartActions = createActionGroup({
	source: "Shopping Cart",
	events: {
		ItemAdded: props<{ item: Item }>(),
		CloseButtonClicked: emptyProps,
		BuyButtonClicked: emptyProps,
		EmptyButtonClicked: emptyProps,
		CheckOutRequested: props<{ items: Item[]; montoTotal: number }>(),
		CheckOutEmpty: emptyProps,
		CheckOutSucceeded: props<{ items: Item[]; montoTotal: number }>(),
	},
});

export const BuyButtonWithItemsAndTotal = createAction(
	"[Shopping Cart] - Shopping Cart buy button clicked",
	props<{ items: Item[]; montoTotal: number }>()
);
export const CheckOutItems = createAction(
	"[Shopping Cart] - Checkout items",
	props<{ items: Item[]; montoTotal: number }>()
);
