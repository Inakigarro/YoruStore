import { createAction, props } from "@ngrx/store";
import { Toolbar } from "@root/components/models";

export const RegisterToolbar = createAction(
    '[Toolbar] - Register toolbar',
    props<{toolbar: Toolbar}>()
);