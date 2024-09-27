import {
	createAction,
	createActionGroup,
	emptyProps,
	props,
} from "@ngrx/store";
import { Item } from "@root/components/models";

export const ItemDetailsActions = createActionGroup({
	source: "Item Details",
	events: {
		ItemLoaded: props<{ item: Item }>(),
		BackButtonClicked: emptyProps,
		BuyButtonClicked: props<{ item: Item }>(),
		AddShoppingCartButtonClicked: props<{ item: Item }>(),
	},
});
