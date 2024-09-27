import { createFeatureSelector, createSelector } from "@ngrx/store";
import { ITEM_DETAILS_FEATURE, ItemDetailsState } from "./item-details.reducer";

export const getItemDetailsState =
	createFeatureSelector<ItemDetailsState>(ITEM_DETAILS_FEATURE);
export const getItemLoaded = createSelector(
	getItemDetailsState,
	(state) => state.loaded
);
export const getItem = createSelector(
	getItemDetailsState,
	(state) => state.item
);
