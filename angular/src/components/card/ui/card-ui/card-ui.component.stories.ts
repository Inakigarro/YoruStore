import { Meta, moduleMetadata, StoryObj } from "@storybook/angular";
import { CardUiComponent } from "./card-ui.component";
import { CardUiModule } from "../card-ui.module";
import { provideMockStore } from "@ngrx/store/testing";

const card: Meta<CardUiComponent> = {
	title: "custom-component/card",
	component: CardUiComponent,
	decorators: [
		moduleMetadata({
			imports: [CardUiModule],
			providers: [provideMockStore()],
		}),
	],
};

export default card;
export type CardStory = StoryObj<CardUiComponent>;

export const BasicCard: CardStory = {
	args: {
		side: false,
		id: "1",
		titulo: "Sarasa",
		descripcion: "Firulete",
		precio: 1000,
	},
};

export const SideCard: CardStory = {
	args: {
		side: true,
		id: "1",
		titulo: "Sarasa",
		descripcion: "Firulete",
		precio: 1000,
	},
};
