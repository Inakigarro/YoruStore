import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { AuthService } from "../auth.service";
import { AuthActions } from "./auth.actions";
import { filter, map, tap } from "rxjs";
import { fetch } from "@ngrx/router-store/data-persistence";

@Injectable()
export class AuthEffects {
	public navigateToLoginPage$ = createEffect(
		() =>
			this.actions$.pipe(
				ofType(AuthActions.userNotLoggedIn),
				tap(() => this.authService.navigate(["login"]))
			),
		{ dispatch: false }
	);

	public loginButtonClicked$ = createEffect(() =>
		this.actions$.pipe(
			ofType(AuthActions.logInButtonClicked),
			fetch({
				run: (action) =>
					this.authService.login(action.login).pipe(
						filter((x) => !!x),
						map((result) =>
							AuthActions.userLoggedIn({
								response: {
									statusCode: result.statusCode,
									message: result.message,
									token: result.token,
								},
							})
						)
					),
			})
		)
	);
	constructor(
		private actions$: Actions,
		private readonly authService: AuthService
	) {}
}
