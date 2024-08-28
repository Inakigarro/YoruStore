import { createFeatureSelector, createSelector } from "@ngrx/store";
import { TOOLBAR_FEATURE_KEY, toolbarAdapter, ToolbarState } from "./toolbar.reducer";

const toolbarAdapterSelectors = toolbarAdapter.getSelectors();
export const toolbarState = createFeatureSelector<ToolbarState>(TOOLBAR_FEATURE_KEY);

export const selectToolbarIds = createSelector(
    toolbarState,
    toolbarAdapterSelectors.selectIds
);

export const selectToolbarEntities = createSelector(
    toolbarState,
    toolbarAdapterSelectors.selectEntities
);

export const selectAllToolbars = createSelector(
    toolbarState,
    toolbarAdapterSelectors.selectAll
);

