from django.http import HttpResponse
from django.shortcuts import render
from .models import Tag
from .models import Article
from .models import Author
from django.utils.html import format_html
from django.utils.html import strip_tags


def index(request):
     return render(
        request,
        'index.html'
    )

def ourteam(request):
    return render(request,'ourteam.html')


def about(request):
    return render(
        request,
            'about.html'
    )

def whattodo(request):
    return render(
        request,
            'whattodo.html'
    )

def tags_and_articles_list(request):
    tags = Tag.objects.all()
    articles = Article.objects.all()
    context = {'tags': tags,
               'articles': articles,
               }
    return render(request,'whattodo.html', context)

def articles_by_tag_list(request,mytag):
    tags = Tag.objects.all()
    tag = tags.get(tag_name=mytag)
    articles_with_tag = tag.article_set.all()
    context = {'tag': mytag,'tags':tags,'articles_with_tag': articles_with_tag}

    return render(request,'by_tag.html', context)


def article(request,myarticleID):
    tags = Tag.objects.all()
    article = Article.objects.get(article_id=myarticleID)
    myarticle_author = article.article_author
    article_content = format_html(article.article_content)
    context = {'tags':tags,
               'article_name': article.article_name,
               'article_content': article_content,
               'article_author_full_name': myarticle_author.author_full_name,
               'article_author_info': myarticle_author.author_info,
               'article_date_added': article.article_date_added,
               'article_source': article.article_source,
               }
    return render(request,'article.html', context)


def author(request,myauthorfullname):
    author = Author.objects.get(author_full_name = myauthorfullname)
    pic_url = '/images/'+ myauthorfullname.replace(" ", "_")+'_0.jpg'
    #pic_url = 'authortuta.jpg'
    context = {'author_info': author.author_info,
                'author_link': author.author_link,
                'author_photo': author.author_photo,
                'author_full_name': author.author_full_name,
                'author_email': author.author_email,
                'pic_url': pic_url

    }
    return render(request,'author.html', context)

def author_img(request, myauthorfullname):
    pic_url = myauthorfullname.replace(" ", "_") + str('0.jpg')
    #ic_url = "http://ya.ru"
    context = {
    'pic_url': pic_url

    }
    return render(request,'author.html',context)
