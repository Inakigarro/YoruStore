import { Categoria, Item } from "@components/models";
import { Action, createReducer, on } from "@ngrx/store";
import { CategoriesActions } from "./categorias.actions";
import { SecondaryToolbarActions } from "@root/app/state/app.actions";

export const CATEGORIES_FEATURE_KEY = "categorias";
export interface CategoriesState {
	currentCategory?: Categoria;
	items?: Item[];
	loaded: boolean;
	error?: any;
}
export interface CategoriesPartialState {
	readonly [CATEGORIES_FEATURE_KEY]: CategoriesState;
}
const initialState: CategoriesState = {
	loaded: false,
};
export const categoriesReducer = createReducer(
	initialState,
	on(SecondaryToolbarActions.categoryButtonClicked, (state) => ({
		...state,
		currentCategory: undefined,
		items: undefined,
		loaded: false,
	})),
	on(CategoriesActions.categoriaCargada, (state, action) => ({
		...state,
		currentCategory: action.categoria,
		items: action.categoria.items,
		loaded: true,
	})),
	on(CategoriesActions.searchButtonClicked, (state) => ({
		...state,
		loaded: false,
		items: [],
	})),
	on(CategoriesActions.searchInputKeyUp, (state, action) => ({
		...state,
		items: state.currentCategory?.items.filter(
			(item) =>
				item.titulo.includes(action.value) ||
				item.descripcion.includes(action.value)
		),
	})),
	on(CategoriesActions.itemFiltered, (state, action) => ({
		...state,
		items: action.items,
		loaded: true,
	}))
);
export function reducer(state: CategoriesState, action: Action) {
	return categoriesReducer(state, action);
}
