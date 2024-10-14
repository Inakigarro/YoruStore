import { createFeatureSelector, createSelector } from "@ngrx/store";
import { APP_STATE_KEY, AppState } from "./app.reducer";
import { filter } from "rxjs";

export const getAppState = createFeatureSelector<AppState>(APP_STATE_KEY);

export const getLoading = createSelector(getAppState, (state) => state.loading);

export const getMenuOpened = createSelector(
	getAppState,
	(state) => state.isMenuOpened
);

export const getShoppingCartOpened = createSelector(
	getAppState,
	(state) => state.isShoppingCartOpened
);

export const getCurrentUserProfile = createSelector(
	getAppState,
	(state) => state.currentUserProfile
);

export const getShoppingCartCount = createSelector(
	getAppState,
	(state) => state.shoppingCartCount
);
