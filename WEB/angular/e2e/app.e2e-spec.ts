import { SimpleTaskAppTemplatePage } from './app.po';

describe('SimpleTaskApp App', function() {
  let page: SimpleTaskAppTemplatePage;

  beforeEach(() => {
    page = new SimpleTaskAppTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
