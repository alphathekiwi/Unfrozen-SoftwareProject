# Generated by Django 3.0a1 on 2019-09-17 00:58

from django.db import migrations


class Migration(migrations.Migration):

    dependencies = [
        ('unfrozen', '0004_auto_20190917_1143'),
    ]

    operations = [
        migrations.RenameField(
            model_name='article',
            old_name='articleContent',
            new_name='article_content',
        ),
        migrations.RenameField(
            model_name='article',
            old_name='articleDateAdded',
            new_name='article_date_added',
        ),
        migrations.RenameField(
            model_name='article',
            old_name='articleSource',
            new_name='article_source',
        ),
        migrations.RenameField(
            model_name='author',
            old_name='author_fullName',
            new_name='author_full_name',
        ),
    ]
