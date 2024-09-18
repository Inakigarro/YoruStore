import { Action, createReducer, on } from '@ngrx/store';
import { categoriesObtained, menuButtonClicked, SecondaryToolbarActions, shoppingCartButtonClicked, userProfileObtained } from './app.actions';
import { UserProfile } from '../identity/models';
import { Categoria } from '@root/components/models';
import { ShoppingCartButtonClicked } from '@components/card/state/card.actions';

export const APP_STATE_KEY = 'app-state';

export interface AppState {
    categories: Categoria[];
    currentUserProfile?: UserProfile;
    currentCategory?: Categoria;
    isMenuOpened: boolean;
    shoppingCartCount: number;
}

export interface AppPartialState {
    readonly [APP_STATE_KEY]: AppState
}

const initialState: AppState = {
    categories: [],
    isMenuOpened: true,
    shoppingCartCount: 0,
};

export const appReducer = createReducer(
    initialState,
    on(categoriesObtained, (state, action) => ({
        ...state,
        categories: action.categorias
    })),
    on(menuButtonClicked, state => ({
        ...state,
        isMenuOpened: !state.isMenuOpened
    })),
    on(userProfileObtained, (state, action) =>({
        ...state,
        currentUserProfile: action.userProfile
    })),
    on(SecondaryToolbarActions.categoriaCargada, (state, action) => ({
        ...state,
        currentCategory: action.categoria
    })),
    on(ShoppingCartButtonClicked, state => ({
        ...state,
        shoppingCartCount: state.shoppingCartCount + 1,
    })),
);

export function reducer(state: AppState, action: Action) {
    return appReducer(state, action);
}