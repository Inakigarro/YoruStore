import { Action, createReducer, on } from "@ngrx/store";
import { DetailsButtonClicked } from "@root/components/card/state/card.actions";
import { Item } from "@root/components/models";
import { ItemDetailsActions } from "./item-details.actions";

export const ITEM_DETAILS_FEATURE = "item-details";
export interface ItemDetailsState {
	loaded: boolean;
	item?: Item;
}
export interface ItemDetailsPartialState {
	readonly [ITEM_DETAILS_FEATURE]: ItemDetailsState;
}
export const initialState: ItemDetailsState = {
	loaded: false,
};
export const reducer = createReducer(
	initialState,
	on(DetailsButtonClicked, (state) => ({
		...state,
		loaded: false,
	})),
	on(ItemDetailsActions.itemLoaded, (state, action) => ({
		...state,
		loaded: true,
		item: action.item,
	}))
);
export function itemDetailsReducer(state: ItemDetailsState, action: Action) {
	return reducer(state, action);
}
