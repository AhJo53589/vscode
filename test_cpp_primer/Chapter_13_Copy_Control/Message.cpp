#include "pch.h"
#include "Message.h"

#include <iostream>
#include <string>
using namespace std;

Message::Message(const Message &m)
	: contents(m.contents), folders(m.folders)
{
	add_to_Folders(m);
}

Message & Message::operator=(const Message &rhs)
{
	remove_from_Folders();
	contents = rhs.contents;
	folders = rhs.folders;
	add_to_Folders(rhs);
	return *this;
}

Message::Message(Message &&m) : contents(std::move(m.contents))
{
	move_Folders(&m);
}

Message & Message::operator=(Message &&rhs)
{
	if (this != &rhs)
	{
		remove_from_Folders();
		contents = std::move(rhs.contents);
		move_Folders(&rhs);
	}
	return *this;
}

Message::~Message()
{
	remove_from_Folders();
}

void Message::save(Folder &f)
{
	folders.insert(&f);
	f.addMsg(this);
}

void Message::remove(Folder &f)
{
	folders.erase(&f);
	f.remMsg(this);
}

void Message::show()
{
	cout << "[Message] content = " << contents << endl;
	size_t i = 0;
	for (auto f : folders)
	{
		cout << "folder[" << i++ << "], id = " << to_string(f->folder_id) << endl;
	}
}

void Message::add_to_Folders(const Message &m)
{
	for (auto f : m.folders)
	{
		f->addMsg(this);
	}
}

void Message::remove_from_Folders()
{
	for (auto f : folders)
	{
		f->remMsg(this);
	}
}

void Message::move_Folders(Message *m)
{
	folders = std::move(m->folders);
	for (auto f : folders)
	{
		f->remMsg(m);
		f->addMsg(this);
	}
	m->folders.clear();
}

void swap(Message & lhs, Message & rhs)
{
	using std::swap;
	for (auto f : lhs.folders)
	{
		f->remMsg(&lhs);
	}
	for (auto f : rhs.folders)
	{
		f->remMsg(&rhs);
	}
	swap(lhs.folders, rhs.folders);
	swap(lhs.contents, rhs.contents);
	for (auto f : lhs.folders)
	{
		f->addMsg(&lhs);
	}
	for (auto f : rhs.folders)
	{
		f->addMsg(&rhs);
	}
}

int Folder::FolderID = 0;

Folder::Folder(const Folder &f)
	: folder_id(FolderID++)
{
	add_all_messages(f);
}

Folder & Folder::operator=(const Folder &f)
{
	Folder temp(f);
	remove_all_messages();
	add_all_messages(temp);
	return *this;
}

Folder::~Folder()
{
	remove_all_messages();
}

void Folder::addMsg(Message *m)
{
	messages.insert(m);
}

void Folder::remMsg(Message *m)
{
	messages.erase(m);
}

void Folder::show()
{
	cout << "[Folder] id = " << to_string(folder_id) << endl;
	size_t i = 0;
	for (auto m : messages)
	{
		cout << "message[" << i++ << "], contents = " << m->contents << endl;
	}
}

void Folder::add_all_messages(const Folder& f)
{
	for (auto m : f.messages)
	{
		m->save(*this);
	}
}

void Folder::remove_all_messages()
{
	while (!messages.empty())
	{
		(*messages.begin())->remove(*this);
	}
}
