# Generated by Django 3.0a1 on 2019-09-21 11:19

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('unfrozen', '0006_auto_20190921_2207'),
    ]

    operations = [
        migrations.AddField(
            model_name='tag',
            name='tag_articles',
            field=models.ManyToManyField(to='unfrozen.Article'),
        ),
    ]