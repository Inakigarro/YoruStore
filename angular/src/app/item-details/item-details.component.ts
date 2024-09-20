import { Component, Input, OnDestroy, OnInit } from "@angular/core";
import { Item, Toolbar } from "@components/models";
import {
	ItemDetailsService,
	DETAILS_CARD_TOOLBAR,
} from "./item-details.service";
import { filter, Observable, Subject, takeUntil } from "rxjs";
import { RegisterToolbar } from "@components/toolbar/state/toolbar.actions";
import { detailsBackButtonClicked } from "./state/item-details.actions";

@Component({
	selector: "item-details",
	templateUrl: "item-details.component.html",
	styleUrl: "item-details.component.scss",
})
export class ItemDetailsComponent implements OnInit, OnDestroy {
	private destroy$ = new Subject<void>();
	public item$: Observable<Item | undefined>;
	public cardToolbar$: Observable<Toolbar | undefined>;

	constructor(private service: ItemDetailsService) {}

	public ngOnInit(): void {
		this.item$ = this.service.currentItem$;
		this.item$
			.pipe(
				takeUntil(this.destroy$),
				filter((x) => !!x)
			)
			.subscribe((item) => {
				this.service.dispatch(
					RegisterToolbar({
						toolbar: {
							id: DETAILS_CARD_TOOLBAR,
							mainButton: {
								type: "icon",
								icon: "arrow_back",
								label: "",
								action: detailsBackButtonClicked(),
							},
							title: item.titulo,
							secondaryButton: [],
							toolbarConfig: {
								isTitleSeparete: true,
								isSecondaryToolbar: false,
							},
						},
					})
				);
			});
		this.cardToolbar$ = this.service.cardToolbar$;
	}

	public ngOnDestroy(): void {
		this.destroy$.next();
		this.destroy$.complete();
	}
}
