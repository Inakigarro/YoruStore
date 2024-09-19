import { createEntityAdapter, EntityAdapter, EntityState } from "@ngrx/entity";
import { Action, createReducer, on } from "@ngrx/store";
import { ShoppingCartButtonClicked } from "@components/card/state/card.actions";
import { Item } from "@components/models";
import { ItemAddedToShoppingCart } from "./shopping-cart.actions";

export const SHOPPING_CART_FEATURE_KEY = "shopping-cart";

export interface ShoppingCartState extends EntityState<Item> {
	total: number;
}

export function selectById(a: Item): string {
	return a.id;
}

export function sortById(a: Item, b: Item): number {
	return a.id.localeCompare(b.id);
}

export const ShoppingCartAdapter: EntityAdapter<Item> =
	createEntityAdapter<Item>({
		selectId: selectById,
		sortComparer: sortById,
	});

export interface ShoppingCartPartialState {
	readonly [SHOPPING_CART_FEATURE_KEY]: ShoppingCartState;
}

export const initialState = ShoppingCartAdapter.getInitialState({
	total: 0,
});

const reducer = createReducer(
	initialState,
	on(ItemAddedToShoppingCart, (state, action) => ({
		...ShoppingCartAdapter.upsertOne(action.item, state),
		total: state.total + action.item.precio,
	}))
);

export function shoppingCartReducer(state: ShoppingCartState, action: Action) {
	return reducer(state, action);
}
