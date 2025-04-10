using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using FinanceService.Domain;

namespace FinanceService.WebApi.Report;

public class ContributionReportGenerator : IContributionReportGenerator
{
    public byte[] Generate(List<(PersonEntity person, List<ContributionEntity> contributions)> data)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Header().Text("Внесенные паевые взносы").SemiBold().FontSize(20).FontColor(Colors.Blue.Darken2);
                page.Content().Element(BuildContent);
                page.Footer().AlignCenter().Text($"Сформировано: {DateTime.Now.AddHours(3):dd.MM.yyyy HH:mm}");
            });

            void BuildContent(IContainer container)
            {
                container.Column(col =>
                {
                    foreach (var (person, contributions) in data)
                    {
                        col.Item().PaddingVertical(10).Text($"{person.Surname} {person.Name}").Bold().FontSize(14);

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Дата").Bold();
                                header.Cell().Text("Сумма").Bold();
                            });

                            foreach (var c in contributions.OrderBy(x => x.Date))
                            {
                                table.Cell().Text(c.Date.ToShortDateString());
                                table.Cell().Text($"{c.Amount} руб.");
                            }
                        });
                    }
                });
            }
        });

        return document.GeneratePdf();
    }
}