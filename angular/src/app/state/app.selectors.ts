import { createFeatureSelector, createSelector } from "@ngrx/store";
import { APP_STATE_KEY, AppState } from "./app.reducer";
import { filter } from "rxjs";

export const getAppState = createFeatureSelector<AppState>(APP_STATE_KEY);

export const getMenuOpened = createSelector(
    getAppState,
    state => state.isMenuOpened
);

export const getCurrentUserProfile = createSelector(
    getAppState,
    state => state.currentUserProfile
);

export const getShoppingCartCount = createSelector(
    getAppState,
    state => state.shoppingCartCount
)

export const getCurrentCategory = createSelector(
    getAppState,
    state => state.currentCategory
);

export const getCurrentCategoryName = createSelector(
    getCurrentCategory,
    cat => cat ? cat.nombre : ''
);

export const getCurrentCategoryItems = createSelector(
    getCurrentCategory,
    cat => cat ? cat.items : []
);