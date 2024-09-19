import { createFeatureSelector, createSelector } from "@ngrx/store";
import { SHOPPING_CART_FEATURE_KEY, ShoppingCartAdapter, ShoppingCartState } from "./shopping-cart.reducer";

const shoppingCartAdapterSelectors = ShoppingCartAdapter.getSelectors();
export const shoppingCartState = createFeatureSelector<ShoppingCartState>(SHOPPING_CART_FEATURE_KEY);

export const selectItemsIds = createSelector(
    shoppingCartState,
    shoppingCartAdapterSelectors.selectIds
)

export const selectItemsEntities = createSelector(
    shoppingCartState,
    shoppingCartAdapterSelectors.selectEntities
)

export const selectAllItems = createSelector(
    shoppingCartState,
    shoppingCartAdapterSelectors.selectAll
)

export const selectMontoTotal = createSelector(
    shoppingCartState,
    state => state.total
)