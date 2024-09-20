import { createAction, props } from "@ngrx/store";
import { Item } from "@root/components/models";

export const detailsBackButtonClicked = createAction(
	"[DetailsCard] - Back button clicked"
);

export const detailsBuyButtonClicked = createAction(
	"[DetailsCard] - Buy button clicked",
	props<{ item: Item }>()
);

export const detailsAddShoppingCartButtonClicked = createAction(
	"[DetailsCard] - Add to shopping cart button clicked",
	props<{ item: Item }>()
);
