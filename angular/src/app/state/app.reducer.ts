import { Action, createReducer, on } from '@ngrx/store';
import { menuButtonClicked, SecondaryToolbarActions, userProfileObtained } from './app.actions';
import { UserProfile } from '../identity/models';
import { Categoria } from '@root/components/models';

export const APP_STATE_KEY = 'app-state';

export interface AppState {
    currentUserProfile?: UserProfile;
    currentCategory?: Categoria;
    isMenuOpened: boolean
}

export interface AppPartialState {
    readonly [APP_STATE_KEY]: AppState
}

const initialState: AppState = {
    isMenuOpened: true,
};

export const appReducer = createReducer(
    initialState,
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
);

export function reducer(state: AppState, action: Action) {
    return appReducer(state, action);
}