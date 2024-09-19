import { createAction, props } from "@ngrx/store";
import { Item } from "@root/components/models";

export const ShoppingCartButtonClicked = createAction(
	"[Item Card] - Shopping cart button clicked",
	props<{ item: Item }>()
);

export const DetailsButtonClicked = createAction(
	"[Item Card] - Details button clicked",
	props<{ itemId: string }>()
);
