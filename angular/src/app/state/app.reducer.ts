import { Action, createReducer, on } from "@ngrx/store";
import {
	backdropClicked,
	MainToolbarActions,
	SecondaryToolbarActions,
} from "./app.actions";
import { UserProfile } from "../identity/models";
import { ShoppingCartButtonClicked } from "@components/card/state/card.actions";
import { ItemDetailsActions } from "../item-details/state/item-details.actions";
import {
	CloseButtonClicked,
	EmptyButtonClicked,
} from "../shopping-cart/state/shopping-cart.actions";
import { Categoria } from "@root/components/models";

export const APP_STATE_KEY = "app-state";

export interface AppState {
	categories: Categoria[];
	currentUserProfile?: UserProfile;
	isMenuOpened: boolean;
	isShoppingCartOpened: boolean;
	shoppingCartCount: number;
	loading: boolean;
}

export interface AppPartialState {
	readonly [APP_STATE_KEY]: AppState;
}

const initialState: AppState = {
	categories: [],
	isMenuOpened: true,
	isShoppingCartOpened: false,
	shoppingCartCount: 0,
	loading: true,
};

export const appReducer = createReducer(
	initialState,
	on(SecondaryToolbarActions.categoriesObtained, (state, action) => ({
		...state,
		categories: action.categories,
		loading: false,
	})),
	on(MainToolbarActions.menuButtonClicked, (state) => ({
		...state,
		isMenuOpened: !state.isMenuOpened,
	})),
	on(MainToolbarActions.shoppingCartButtonClicked, (state) => ({
		...state,
		isShoppingCartOpened: !state.isShoppingCartOpened,
	})),
	on(backdropClicked, CloseButtonClicked, (state) => ({
		...state,
		isShoppingCartOpened: false,
	})),
	on(
		ShoppingCartButtonClicked,
		ItemDetailsActions.addShoppingCartButtonClicked,
		(state) => ({
			...state,
			shoppingCartCount: state.shoppingCartCount + 1,
		})
	),
	on(EmptyButtonClicked, (state) => ({
		...state,
		shoppingCartCount: 0,
	}))
);

export function reducer(state: AppState, action: Action) {
	return appReducer(state, action);
}
