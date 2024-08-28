import { createFeatureSelector, createSelector } from "@ngrx/store";
import { APP_STATE_KEY, AppState } from "./app.reducer";

export const getAppState = createFeatureSelector<AppState>(APP_STATE_KEY);

export const getMenuOpened = createSelector(
    getAppState,
    state => state.isMenuOpened
);