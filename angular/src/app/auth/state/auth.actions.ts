import { createActionGroup, emptyProps, props } from "@ngrx/store";
import { Login, LoginResponse } from "@root/components/models";

export const AuthActions = createActionGroup({
	source: "Auth",
	events: {
		LogInButtonClicked: props<{ login: Login }>(),
		UserLoggedIn: props<{ response: LoginResponse }>(),
		CancelButtonClicked: emptyProps,
		UserNotLoggedIn: emptyProps(),
	},
});
