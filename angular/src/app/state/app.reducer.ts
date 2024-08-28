import { Action, createReducer, on } from '@ngrx/store';
import { menuButtonClicked } from './app.actions';

export const APP_STATE_KEY = 'app-state';

export interface AppState {
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
);

export function reducer(state: AppState, action: Action) {
    return appReducer(state, action);
}