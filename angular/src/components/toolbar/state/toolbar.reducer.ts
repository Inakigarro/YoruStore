import { createEntityAdapter, EntityAdapter, EntityState } from '@ngrx/entity';
import { Action, createReducer, on } from '@ngrx/store';
import { Toolbar } from '@root/components/models';
import { RegisterToolbar } from './toolbar.actions';

export const TOOLBAR_FEATURE_KEY = 'toolbars';

export interface ToolbarState extends EntityState<Toolbar> {
    
}

export function selectById(a: Toolbar): string {
    return a.id;
}

export function sortById(a: Toolbar, b: Toolbar): number {
    return a.id.localeCompare(b.id);
}

export const toolbarAdapter: EntityAdapter<Toolbar> = createEntityAdapter<Toolbar> ({
    selectId: selectById,
    sortComparer: sortById
});

export interface ToolbarPartialState {
    readonly [TOOLBAR_FEATURE_KEY]: ToolbarState
}

export const initialState = toolbarAdapter.getInitialState();

const reducer = createReducer(
    initialState,
    on(RegisterToolbar, (state, action) => ({
        ...toolbarAdapter.addOne(action.toolbar, state)
    }))
);

export function toolbarReducer(state: ToolbarState, action: Action){
    return reducer(state, action);
}